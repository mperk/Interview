import { Component, OnInit, Input, Injector } from '@angular/core';
import { UserDto } from '@shared/service-proxies/service-proxies';
import { PagedListingComponentBase } from '@shared/paged-listing-component-base';
import { PagedRequestDto } from '../../../shared/paged-listing-component-base';
import { OrganizationUnitService } from '../services/organization-unit.service';

@Component({
  selector: 'app-users-in-ou',
  templateUrl: './users-in-ou.component.html',
  styleUrls: ['./users-in-ou.component.css']
})
export class UsersInOuComponent extends PagedListingComponentBase<UserDto> {

  @Input() users: UserDto[] = [];
  @Input() organizationUnitId: number;

  constructor(injector: Injector,
    private _organizationUnitService: OrganizationUnitService) {
    super(injector);
  }

  ngOnInit() {
  }


  protected list(request: PagedRequestDto, pageNumber: number, finishedCallback: Function): void {
    this._organizationUnitService.getUsers(request, this.organizationUnitId)
      .subscribe((response) => {
        this.users = response.result;
      });
  }
  protected delete(entity: UserDto): void {
    throw new Error('Method not implemented.');
  }

}
