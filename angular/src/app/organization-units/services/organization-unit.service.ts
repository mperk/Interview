import { Injectable } from '@angular/core';
import { HttpClientService } from '@shared/http/http-client.service';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class OrganizationUnitService {
    constructor(private http: HttpClientService) { }

    create(displayName: string, parentId?: number): Observable<any> {
        return this.http.post('/api/services/app/OrganizationUnit/Create', {
            ParentId: parentId,
            DisplayName: displayName
        });
    }

    getList(): Observable<any> {
        return this.http.get('/api/services/app/OrganizationUnit/GetList');
    }

    getTreeList(): Observable<any> {
        return this.http.get('/api/services/app/OrganizationUnit/GetTreeList');
    }

    updateDisplayName(id: number, displayName: string): Observable<any> {
        return this.http.put('/api/services/app/OrganizationUnit/UpdateDisplayName', {
            Id: id,
            DisplayName: displayName
        });
    }

    delete(id: number): Observable<any> {
        return this.http.delete('/api/services/app/OrganizationUnit/Delete?id='+id);
    }

    addSubUnit(displayName: string, parentId: number): Observable<any> {
        return this.create(displayName, parentId);
    }
}
