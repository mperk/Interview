import { Component, OnInit, Injector, Input, Inject, Optional, Output, EventEmitter } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from 'shared/paged-listing-component-base';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { OrganizationUnitService } from '@app/organization-units/services/organization-unit.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { finalize } from 'rxjs/operators';
import { SelectionModel } from '@angular/cdk/collections';

@Component({
  selector: 'app-modal-add-users',
  templateUrl: './modal-add-users.component.html',
  styleUrls: ['./modal-add-users.component.css']
})
export class ModalAddUsersComponent extends PagedListingComponentBase<UserDto> {

  users: UserDto[] = [];
  selection = new SelectionModel<UserDto>(true, []);
  @Output() eventAfterSave: EventEmitter<any> = new EventEmitter<any>();
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
        this.showPaging(response.result, pageNumber);
      });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

  /** Whether the number of selected elements matches the total number of rows. */
  isAllSelected() {
    const numSelected = this.selection.selected.length;
    const numRows = this.users.length;
    return numSelected === numRows;
  }

  /** Selects all rows if they are not all selected; otherwise clear selection. */
  masterToggle() {
    this.isAllSelected() ?
      this.selection.clear() :
      this.users.forEach(row => this.selection.select(row));
  }

  addUsers() {
    const userIds = this.selection.selected.map(({ id }) => id);
    this._organizationUnitService.addUsersInOu(userIds, this.data.organizationUnitId)
      .subscribe((response) => {
        this.close(true);
      });
  }

  protected delete(entity: UserDto): void {
    
  }
}
