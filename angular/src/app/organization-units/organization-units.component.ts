import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { PagedListingComponentBase, PagedRequestDto } from '@shared/paged-listing-component-base';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { OrganizationUnitDto } from './models/organizationUnitDto';
import { MatDialog, MatTreeNestedDataSource, MatMenuTrigger } from '@angular/material';
import { ModalOuComponent } from './modal-ou/modal-ou.component';
import { OrganizationUnitService } from './services/organization-unit.service';
import { AppComponentBase } from '@shared/app-component-base';
import { NestedTreeControl } from '@angular/cdk/tree';

@Component({
  selector: 'app-organization-units',
  templateUrl: './organization-units.component.html',
  styleUrls: ['./organization-units.component.css'],
  animations: [appModuleAnimation()]
})
export class OrganizationUnitsComponent extends AppComponentBase {

  organizationUnits: OrganizationUnitDto[] = [];
  dataSource = new MatTreeNestedDataSource<OrganizationUnitDto>();
  treeControl = new NestedTreeControl<OrganizationUnitDto>(node => node.children);
  hasChild = (_: number, node: OrganizationUnitDto) => !!node.children && node.children.length > 0;
  @ViewChild(MatMenuTrigger, { static: true }) contextMenu: MatMenuTrigger;
  contextMenuPosition = { x: '0px', y: '0px' };
  constructor(
    injector: Injector,
    private _dialog: MatDialog,
    private _organizationUnitService: OrganizationUnitService,

  ) {
    super(injector);
  }

  ngOnInit() {
    this.list();
  }

  protected list(): void {
    this._organizationUnitService.getTreeList()
      .subscribe((response) => {
        this.organizationUnits = response.result.items;
        this.dataSource.data = this.organizationUnits;
      });
  }

  createOrganizationUnit(): void {
    this.showOrganizationUnitDialog();
  }

  editOrganizationUnit(organizationUnit: OrganizationUnitDto): void {
    this.showOrganizationUnitDialog(organizationUnit);
  }

  showOrganizationUnitDialog(organizationUnit?: OrganizationUnitDto): void {
    let createOrEditOUDialog;
    if (organizationUnit === undefined) {
      createOrEditOUDialog = this._dialog.open(ModalOuComponent);
    } else {
      createOrEditOUDialog = this._dialog.open(ModalOuComponent, {
        data: { organizationUnit: organizationUnit }
      });
    }

    createOrEditOUDialog.afterClosed().subscribe(result => {
      if (result) {
        this.list();
      }
    });
  }

  onRightClick(event, node) {
    event.preventDefault();
    this.contextMenuPosition.x = event.clientX + 'px';
    this.contextMenuPosition.y = event.clientY + 'px';
    this.contextMenu.menuData = { 'item': node };
    this.contextMenu.openMenu();
  }


  addSubOrganizationUnit(organizationUnit: OrganizationUnitDto) {
  }

  deleteOrganizationUnit(organizationUnit: OrganizationUnitDto): void {
    abp.message.confirm(
      this.l('', organizationUnit.displayName),
      (result: boolean) => {
        if (result) {
          this._organizationUnitService.delete(organizationUnit.id)
            .subscribe((result) => {
              this.list();
              abp.notify.success(this.l('SuccessfullyDeleted'));
            });
        }
      }
    );
  }
}
