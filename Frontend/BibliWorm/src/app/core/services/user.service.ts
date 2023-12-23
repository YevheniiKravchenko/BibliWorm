import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { api, extractData } from '../constants/api-constants';
import { RegisterUserModel } from '../models/user/register-user-model';
import { UserProfileModel } from '../models/user/user-profile-model';
import { SetUserRoleModel } from '../models/user/set-user-role-model';
import { UserProfileInfo } from '../models/user/user-profile-info';

@Injectable({
    providedIn: 'root'
})
export class UserService {

    constructor(
        private http: HttpClient
    ) { }

    public register(registerUserModel: RegisterUserModel): Observable<any> {
        var url = api.user.register;
        registerUserModel.profilePicture = '';

        return this.http.post(
            url, 
            registerUserModel
        ).pipe(extractData());
    }

    public getAll(searchQuery: string): Observable<UserProfileModel[]> {
        var url = api.user.getAllUsers;
        var params = new HttpParams().set('searchQuery', searchQuery);

        return this.http.get(
            url, 
            { params: params }
        ).pipe(extractData<UserProfileModel[]>());
    }

    public setUserRole(setUserRoleModel: SetUserRoleModel): Observable<any> {
        var url = api.user.setUserRole;

        return this.http.post(
            url, 
            setUserRoleModel
        ).pipe(extractData());
    }

    public getUserProfileById(userId: number): Observable<UserProfileModel> {
        var url = api.user.getUserProfileById;
        var params = new HttpParams().set('userId', userId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<UserProfileModel>());
    }

    public updateReaderCard(userProfileInfo: UserProfileInfo): Observable<any> {
        var url = api.user.updateReaderCard;
        userProfileInfo.profilePicture = '';

        return this.http.post(url, userProfileInfo).pipe(extractData());
    }
}
