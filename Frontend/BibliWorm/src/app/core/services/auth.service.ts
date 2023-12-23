import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginModel } from '../models/auth/login-model';
import { Observable } from 'rxjs';
import { Token } from '../models/auth/token';
import { api, extractData } from '../constants/api-constants';

@Injectable({
    providedIn: 'root'
})
export class AuthService {

    constructor(
        private http: HttpClient
    ) { }

    public login(loginModel: LoginModel): Observable<Token> {
        var url = api.auth.login;

        return this.http.post(url, loginModel).pipe(extractData<Token>());
    }

    public refresh(refreshToken: string): Observable<Token> {
        var url = api.auth.refresh;
        var options = {
            params: new HttpParams(),
            headers: new HttpHeaders().append('refreshTokenString', refreshToken),
        };

        return this.http.get(url, options).pipe(extractData<Token>());
    }
}
