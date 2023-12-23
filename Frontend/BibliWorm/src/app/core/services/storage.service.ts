import { Injectable } from '@angular/core';
import { Token } from '../models/auth/token';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Role } from '../enums/role';

const USER_TOKEN = 'auth-user';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

    constructor(
        private jwtHelper: JwtHelperService
    ) { }

    public clear(): void {
        window.sessionStorage.clear();
    }

    public saveToken(data: Token): void {
        window.sessionStorage.removeItem(USER_TOKEN);
        window.sessionStorage.setItem(USER_TOKEN, JSON.stringify(data));
    }

    public getToken(): Token {
        var data = window.sessionStorage.getItem(USER_TOKEN);
        if (data) {
            return JSON.parse(data) as Token;
        }

        return {
            accessToken: "",
            refreshToken: ""
        } as Token;
    }

    public isLoggedIn(): boolean {
        var user = window.sessionStorage.getItem(USER_TOKEN);
        if (user) {
            return true;
        }

        return false;
    }

    public isAdmin(): boolean {
        if (!this.isLoggedIn()){
            return false;
        }

        var currentUserRole = this.getCurrentUserRole();

        return currentUserRole === Role[Role.Admin];
    }

    public isLibrarianOrAdmin(): boolean {
        if (!this.isLoggedIn()){
            return false;
        }

        var currentUserRole = this.getCurrentUserRole();

        return currentUserRole === Role[Role.Admin] 
            || currentUserRole === Role[Role.Librarian];
    }

    private getCurrentUserRole(): string {
        var user = window.sessionStorage.getItem(USER_TOKEN);
        var token;
        if (user != null) {
            token = JSON.parse(user)
        }

        var accessToken = token['accessToken'];
        var decode = this.jwtHelper.decodeToken(accessToken);
        var currentUserRole = decode['role'];

        return currentUserRole;
    }
}
