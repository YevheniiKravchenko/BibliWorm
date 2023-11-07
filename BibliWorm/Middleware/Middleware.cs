using System.Globalization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Common.Configs;
using Common.IoC;
using DAL.DbContexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.Filters;

namespace BibliWorm.Middleware;

public static class Middleware
{
    public static WebApplicationBuilder DefaultConfiguration(
         this WebApplicationBuilder builder)
    {
        var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterBuildCallback(ctx => IoC.Container = ctx.Resolve<ILifetimeScope>());

            BLL.Startup.BootStrapper.Bootstrap(containerBuilder);
        });

        builder.Host.ConfigureAppConfiguration(config =>
        {
            config.AddJsonFile(@"config.json");
        });

        var connectionModel = builder.Configuration.GetSection("Connection").Get<ConnectionModel>();

        var dbHost = Environment.GetEnvironmentVariable("DB_HOST") ?? connectionModel.Host;
        var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? connectionModel.Database;
        var dbPassword = Environment.GetEnvironmentVariable("MSSQL_SA_PASSWORD") ?? connectionModel.Password;

        var connection = string.Format(connectionModel.ConnectionString, dbHost, dbName, dbPassword);

        builder.Services.AddDbContext<DbContextBase>(options => options.UseSqlServer(connection));

        var authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>();
        authOptions.PrivateKeyString = builder.Configuration.GetSection("PrivateKeyString").Get<string>();

        builder.Services.AddSingleton(authOptions);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.PublicKey,
                    ClockSkew = TimeSpan.Zero
                };
            });

        var emailCreds = builder.Configuration.GetSection("EmailCreds").Get<EmailCreds>();
        builder.Services.AddSingleton(emailCreds);

        #region Init Mapper Profiles

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(new[] {
                "DAL",
                "BLL",
            });
        });

        var mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        #endregion

        #region Localization

        builder.Services.AddLocalization();
        builder.Services.Configure<RequestLocalizationOptions>(
            options =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("en-US")
                    {
                        DateTimeFormat = {
                            LongTimePattern = "MM/DD/YYYY",
                            ShortTimePattern = "MM/DD/YYYY"
                        }
                    },
                    new CultureInfo("uk-UA")
                    {
                        DateTimeFormat = {
                            LongTimePattern = "DD/MM/YYYY",
                            ShortTimePattern = "DD/MM/YYYY"
                        }
                    },
                };

                options.DefaultRequestCulture = new RequestCulture(culture: "uk-UA", uiCulture: "uk-UA");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

        #endregion

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            options.OperationFilter<SecurityRequirementsOperationFilter>();
        });


        builder.Services.AddCors(option =>
        {
            option.AddPolicy(name: MyAllowSpecificOrigins, builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        return builder;
    }

}