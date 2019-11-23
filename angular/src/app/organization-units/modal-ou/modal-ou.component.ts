import { Component, OnInit, Injector, Inject, Optional } from '@angular/core';
import { AppComponentBase } from '@shared/app-component-base';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { OrganizationUnitDto } from '../models/organizationUnitDto';
import { OrganizationUnitService } from '../services/organization-unit.service';

@Component({
  selector: 'app-modal-ou',
  templateUrl: './modal-ou.component.html'
})
export class ModalOuComponent extends AppComponentBase implements OnInit {
  organizationUnit: OrganizationUnitDto = new OrganizationUnitDto();
  constructor(
    injector: Injector,
    private _dialogRef: MatDialogRef<ModalOuComponent>,
    private _organizationUnitService: OrganizationUnitService,
    @Optional() @Inject(MAT_DIALOG_DATA) private data: any
  ) {
    super(injector);
  }

  ngOnInit() {
  }

  save(): void {
    console.log(this.data)
    if (this.data === undefined || this.data === null) {
      this._organizationUnitService.createOrganizationUnit(
        this.organizationUnit.displayName, null).subscribe((result) => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close(true);
        });
    }
    // this.saving = true;

    // this.role.grantedPermissions = this.getCheckedPermissions();

    // const role_ = new CreateRoleDto();
    // role_.init(this.role);

    // this._roleService
    //   .create(role_)
    //   .pipe(
    //     finalize(() => {
    //       this.saving = false;
    //     })
    //   )
    //   .subscribe(() => {
    //     this.notify.info(this.l('SavedSuccessfully'));
    //     this.close(true);
    //   });
  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
