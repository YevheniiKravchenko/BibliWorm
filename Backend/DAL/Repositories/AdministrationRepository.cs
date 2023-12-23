using Common.Configs;
using DAL.Contracts;
using DAL.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories;
public class AdministrationRepository : IAdministrationRepository
{
    private readonly ConnectionModel _connectionModel;
    private readonly DbContextBase _context;

    public AdministrationRepository(
        ConnectionModel connectionModel,
        DbContextBase context)
    {
        _connectionModel = connectionModel;
        _context = context;
    }

    public void BackupDatabase(string savePath)
    {
        var databaseName = _connectionModel.Database;
        var fullSavePath = string.Format(@"{0}\{1}.bak", savePath, databaseName);
        var query = string.Format(@"BACKUP DATABASE {0} TO DISK = '{1}'", databaseName, fullSavePath);

        _context.Database.ExecuteSqlRaw(query);
    }
}
