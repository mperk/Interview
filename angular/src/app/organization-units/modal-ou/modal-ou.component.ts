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
    if (this.data && this.data.organizationUnit) {
      // for update
      this.organizationUnit.id = this.data.organizationUnit.id;
      this.organizationUnit.displayName = this.data.organizationUnit.displayName;
    }
  }

  save(): void {
    if (this.data && this.data.organizationUnit) {
      // update
      this._organizationUnitService.updateDisplayName(this.organizationUnit.id, this.organizationUnit.displayName)
        .subscribe((result) => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close(true);
        });
    } else if (this.data && this.data.parentOrganizationUnit){
      // add sub-unit
      this._organizationUnitService.addSubUnit(this.organizationUnit.displayName, this.data.parentOrganizationUnit.id)
        .subscribe((result) => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close(true);
        });
    }
    else {
      // create
      this._organizationUnitService.create(this.organizationUnit.displayName, null)
        .subscribe((result) => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.close(true);
        });
    }

  }

  close(result: any): void {
    this._dialogRef.close(result);
  }

}
