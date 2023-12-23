import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EnumItemModel } from '../models/enum-item/enum-item-model';
import { api, extractData } from '../constants/api-constants';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EnumItemService {

    constructor(
        private http: HttpClient,
    ) { }

    public getAllEnumItems(): Observable<EnumItemModel[]> {
        var url = api.enumItem.getAllEnumItems;

        return this.http.get(url).pipe(extractData<EnumItemModel[]>());
    }

    public addEnumItem(enumItemModel: EnumItemModel): Observable<any> {
        var url = api.enumItem.addEnumItem;

        return this.http.post(
            url,
            enumItemModel,
        ).pipe(extractData());
    }

    public deleteEnumItem(enumItemId: number): Observable<any> {
        var url = api.enumItem.deleteEnumItem;
        var params = new HttpParams().set('enumItemId', enumItemId);

        return this.http.delete(url, { params: params })
            .pipe(extractData());
    }

    public getEnumItemById(enumItemId: number): Observable<EnumItemModel> {
        var url = api.enumItem.getEnumItemById;
        var params = new HttpParams().set('enumItemId', enumItemId);

        return this.http.get(url, { params: params })
            .pipe(extractData<EnumItemModel>());
    }

    public updateEnumItem(enumItemModel: EnumItemModel): Observable<any> {
        var url = api.enumItem.updateEnumItem;
        
        return this.http.post(
            url,
            enumItemModel,
        ).pipe(extractData());
    }
}
