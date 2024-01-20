import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatSelectionList } from '@angular/material/list';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { ListRole } from 'src/app/contracts/role/List_Role';
import { AuthorizationEndpointService } from 'src/app/services/common/models/authorization-endpoint.service';
import { RoleService } from 'src/app/services/common/models/role.service';
import { BaseDialog } from '../base/base-dialog';

@Component({
  selector: 'app-authorize-user-dialog',
  templateUrl: './authorize-user-dialog.component.html',
  styleUrls: ['./authorize-user-dialog.component.scss']
})
export class AuthorizeUserDialogComponent extends BaseDialog<AuthorizeUserDialogComponent> implements OnInit {

  constructor(
    dialogRef: MatDialogRef<AuthorizeUserDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: any,
    private roleService: RoleService,
    private authorizationEndpointService: AuthorizationEndpointService,
    private spinnerService: NgxSpinnerService
  ) {
    super(dialogRef);
  }
  async ngOnInit(): Promise<void> {
    this.assignedRoles = await this.authorizationEndpointService.getRolesToEndpoint(this.data.code, this.data.menuName);    
    
    this.response = await this.roleService.getRoles(-1, -1);

    this.listRoles = this.response.roles.map((role) => {
      return {
        name: role.name,
        selected: this.assignedRoles.includes(role.name),
      };
    });
  }

  assignedRoles : string[] = []
  listRoles: {name: string, selected: boolean}[] = [] 

  response: { roles: ListRole[]; totalCount: number };

  assignRoles(rolesComponent: MatSelectionList) {
    const roles: string[] = rolesComponent.selectedOptions.selected.map(
      (x) => x._text.nativeElement.innerText
    );
    this.spinnerService.show(SpinnerType.BallAtom);

    this.authorizationEndpointService.assignRoleEndpoint(
      roles,
      this.data.code,
      this.data.menuName,
      () => {
        this.spinnerService.hide(SpinnerType.BallAtom);
      },
      (error) => {}
    );
  }

}
