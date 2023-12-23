import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateUpdateDepartmentModel } from '../models/department/create-update-department-model';
import { Observable } from 'rxjs';
import { api, extractData } from '../constants/api-constants';
import { DepartmentModel } from '../models/department/department-model';

@Injectable({
    providedIn: 'root'
})

export class DepartmentService {

    constructor(
        private http: HttpClient,
    ) { }

    public addDepartment(departmentModel: CreateUpdateDepartmentModel): Observable<any> {
        var url = api.department.addDepartment;

        return this.http.post(
            url, 
            departmentModel
        ).pipe(extractData());
    }

    public deleteDepartment(departmentId: number): Observable<any> {
        var url = api.department.deleteDepartment;
        var params = new HttpParams().set('departmentId', departmentId);

        return this.http.delete(
            url,
            { params: params }
        ).pipe(extractData());
    }

    public getAllDepartments(): Observable<DepartmentModel[]> {
        var url = api.department.getAllDepartments;

        return this.http.get(url)
            .pipe(extractData<DepartmentModel[]>());
    }

    public getDepartmentById(departmentId: number): Observable<DepartmentModel> {
        var url = api.department.getDepartmentById;
        var params = new HttpParams().set('departmentId', departmentId);

        return this.http.get(
            url,
            { params: params }
        ).pipe(extractData<DepartmentModel>())
    }

    public updateDepartment(departmentModel: CreateUpdateDepartmentModel): Observable<any> {
        var url = api.department.updateDepartment;
        
        return this.http.post(
            url,
            departmentModel
        ).pipe(extractData());
    }
}
