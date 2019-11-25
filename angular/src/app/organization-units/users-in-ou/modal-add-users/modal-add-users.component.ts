import { Component, OnInit, Injector, Input, Inject, Optional } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { OrganizationUnitService } from '@app/organization-units/services/organization-unit.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';

@Component({
  selector: 'app-modal-add-users',
  templateUrl: './modal-add-users.component.html',
  styleUrls: ['./modal-add-users.component.css']
})
export class ModalAddUsersComponent extends PagedListingComponentBase<UserDto> {

  users: UserDto[] = [];

  constructor(injector: Injector,
    private _organizationUnitService: OrganizationUnitService,
    private _dialogRef: MatDialogRef<ModalAddUsersComponent>,
    @Optional() @Inject(MAT_DIALOG_DATA) private data: any) {
    super(injector);
  }

  ngOnInit() {
    this.refresh();
  }

  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._organizationUnitService.getUsersNotInOu(request, this.data.organizationUnitId)
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

  close(result: any): void {
    this._dialogRef.close(result);
  }

  protected delete(entity: UserDto): void {
    throw new Error('Method not implemented.');
  }
}
