import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { api, extractData } from '../constants/api-constants';

@Injectable({
    providedIn: 'root'
})
export class AdministrationService {

    constructor(
        private http: HttpClient
    ) { }

    public backupDatabase(savePath: string): Observable<any> {
        var url = api.administration.backupDatabase;

        return this.http.post(url, { savePath: savePath })
            .pipe(extractData());
    }
}
