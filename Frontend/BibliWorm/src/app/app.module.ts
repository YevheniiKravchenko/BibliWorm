import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { JwtModule, JwtHelperService } from '@auth0/angular-jwt';
import { ToastrModule } from 'ngx-toastr';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslationLoader, l10nConfig } from './l10n-config';
import { L10N_CONFIG, L10N_LOCALE, L10nConfig, L10nDateDirective, L10nIntlModule, L10nLoader, L10nLocale, L10nTranslationModule, L10nTranslationService } from 'angular-l10n';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HeaderComponent } from './components/shared/header/header.component';
import { FooterComponent } from './components/shared/footer/footer.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { LoginComponent } from './components/login/login.component';
import { httpInterceptorProviders } from './core/helpers/http-interceptor.service';
import { UsersComponent } from './components/users/users.component';
import { UserEditComponent } from './components/user-edit/user-edit.component';
import { DepartmentsComponent } from './components/departments/departments.component';
import { DepartmentEditComponent } from './components/department-edit/department-edit.component';
import { GenresComponent } from './components/genres/genres.component';
import { GenreEditComponent } from './components/genre-edit/genre-edit.component';
import { BooksComponent } from './components/books/books.component';
import { BookEditComponent } from './components/book-edit/book-edit.component';
import { BookCopiesComponent } from './components/book-copies/book-copies.component';
import { BookCopyEditComponent } from './components/book-copy-edit/book-copy-edit.component';
import { UserBookingsComponent } from './components/user-bookings/user-bookings.component';
import { AdministrationComponent } from './components/administration/administration.component';
import { createTranslateLoader } from './core/helpers/helpers';


@NgModule({
    declarations: [
        AppComponent,
        HeaderComponent,
        FooterComponent,
        RegistrationComponent,
        LoginComponent,
        UsersComponent,
        UserEditComponent,
        DepartmentsComponent,
        DepartmentEditComponent,
        GenresComponent,
        GenreEditComponent,
        BooksComponent,
        BookEditComponent,
        BookCopiesComponent,
        BookCopyEditComponent,
        UserBookingsComponent,
        AdministrationComponent
    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        NgbModule,
        HttpClientModule,
        FormsModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot({
            positionClass: 'toast-bottom-right',
            closeButton: true,
            progressBar: true
        }),
        JwtModule.forRoot({
            config: {
                tokenGetter: () => localStorage.getItem('access_token'),
                allowedDomains: ['example.com'],
                disallowedRoutes: ['example.com/login'],
            },
        }),
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: createTranslateLoader,
                deps: [HttpClient]
            }
        }),
        L10nTranslationModule.forRoot(
            l10nConfig,
            {
              translationLoader: TranslationLoader
            }
        ),
        L10nIntlModule,
        L10nDateDirective
    ],
    exports: [
        L10nTranslationModule
    ],
    providers: [
        httpInterceptorProviders,
        JwtHelperService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
