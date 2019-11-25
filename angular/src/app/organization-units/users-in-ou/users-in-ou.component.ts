import { Component, OnInit, Input, Injector } from '@angular/core';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { PagedRequestDto } from '../../../shared/paged-listing-component-base';
import { OrganizationUnitService } from '../services/organization-unit.service';
import { MatDialog } from '@angular/material';
import { ModalAddUsersComponent } from './modal-add-users/modal-add-users.component';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-users-in-ou',
  templateUrl: './users-in-ou.component.html',
  styleUrls: ['./users-in-ou.component.css']
})
export class UsersInOuComponent extends PagedListingComponentBase<UserDto> {

  @Input() users: UserDto[] = [];
  @Input() organizationUnitId: number;

  constructor(injector: Injector,
    private _dialog: MatDialog,
    private _organizationUnitService: OrganizationUnitService) {
    super(injector);
  }

  ngOnInit() {
    
  }


  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._organizationUnitService.getUsersInOu(request, this.organizationUnitId)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((response) => {
        this.users = response.result.items;
        this.showPaging(response, pageNumber);
      });
  }

  protected delete(entity: UserDto): void {
    throw new Error('Method not implemented.');
  }

  openAddMemberDialog() {
    let createOrEditOUDialog = this._dialog.open(ModalAddUsersComponent, {
      data: { organizationUnitId: this.organizationUnitId }
    });
    createOrEditOUDialog.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }
}
