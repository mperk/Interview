import { Injectable } from '@angular/core';
import { HttpClientService } from '@shared/http/http-client.service';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root',
})
export class OrganizationUnitService {
    constructor(private http: HttpClientService) { }

    createOrganizationUnit(displayName: string, parentId?: number): Observable<any> {
        return this.http.post('/api/services/app/OrganizationUnit/Create/', {
            ParentId: parentId,
            DisplayName: displayName
        });
    }

    // deleteRoleToUser(userId: string, roleId: number): any {
    //     const options = {
    //         headers: new HttpHeaders({
    //             'Content-Type': 'application/json',
    //         }),
    //         body: {
    //             ROLE_ID: [roleId]
    //         },
    //     };
    //     this.http.delete('/ums/DeleteRolesFromUser/' + userId, options)
    //         .subscribe();
    // }

    // addPermissionsToUser(userId: string, permissionIds) {
    //     this.http.post('/ums/UserPermissions/' + userId, {
    //         PERMISSION_ID: permissionIds
    //     }).subscribe();
    // }

    // deletePermissionsFromUser(userId: string, permissionIds) {
    //     const options = {
    //         headers: new HttpHeaders({
    //             'Content-Type': 'application/json',
    //         }),
    //         body: {
    //             PERMISSION_ID: permissionIds
    //         },
    //     };
    //     this.http.delete('/ums/UserPermissions/' + userId, options)
    //         .subscribe();
    // }
}
