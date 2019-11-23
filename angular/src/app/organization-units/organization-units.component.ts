import { Component, OnInit, Injector } from '@angular/core';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { OrganizationUnitDto } from './models/organizationUnitDto';
import { MatDialog } from '@angular/material';
import { ModalOuComponent } from './modal-ou/modal-ou.component';

@Component({
  selector: 'app-organization-units',
  templateUrl: './organization-units.component.html',
  animations: [appModuleAnimation()]
})
export class OrganizationUnitsComponent extends PagedListingComponentBase<OrganizationUnitDto> {


  constructor(
    injector: Injector,
    private _dialog: MatDialog
    // private _rolesService: RoleServiceProxy,

  ) {
    super(injector);
  }

  ngOnInit() {
  }

  createOrganizationUnit(): void {
    this.showCreateOrEditOrganizationUnitDialog();
  }

  editOrganizationUnit(organizationUnit: OrganizationUnitDto): void {
    this.showCreateOrEditOrganizationUnitDialog(organizationUnit.id);
  }

  showCreateOrEditOrganizationUnitDialog(id?: number): void {
    let createOrEditOUDialog;
    if (id === undefined || id <= 0) {
      createOrEditOUDialog = this._dialog.open(ModalOuComponent);
    } else {
      createOrEditOUDialog = this._dialog.open(ModalOuComponent, {
        data: {id: id, isCreate: false}
      });
    }

    createOrEditOUDialog.afterClosed().subscribe(result => {
      if (result) {
        this.refresh();
      }
    });
  }

  protected list(request: import("../../shared/paged-listing-component-base").PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    throw new Error('Method not implemented.');
  }
  protected delete(entity: OrganizationUnitDto): void {
    throw new Error('Method not implemented.');
  }



}
