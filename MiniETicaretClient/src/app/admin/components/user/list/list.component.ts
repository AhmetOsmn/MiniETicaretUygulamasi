import { Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { ListUser } from 'src/app/contracts/users/list_user';
import { AuthorizeUserDialogComponent } from 'src/app/dialogs/authorize-user-dialog/authorize-user-dialog.component';
import { AlertifyService, MessageType, Position } from 'src/app/services/admin/alertify.service';
import { DialogService } from 'src/app/services/common/dialog.service';
import { UserService } from 'src/app/services/common/models/user.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  constructor(
    spinner: NgxSpinnerService,    
    private alertify: AlertifyService,
    private dialogService: DialogService,
    private userService: UserService 
  ) {
    super(spinner);
  }

  displayedColumns: string[] = [
    'userName',
    'nameSurname',
    'email',
    'twoFactorEnabled',
    'role',
    'delete'
  ];

  dataSource: MatTableDataSource<ListUser> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  
  async getUsers() {
    this.showSpinner(SpinnerType.BallAtom);

    const allUsers: {totalCount: number; listUsers: ListUser[]} = await this.userService.getAllUsers(
      this.paginator ? this.paginator.pageIndex : 0,
      this.paginator ? this.paginator.pageSize : 5,
      () => this.hideSpinner(SpinnerType.BallAtom),
      (errorMessage) =>
        this.alertify.message(errorMessage, {
          dismisssOthers: true,
          messageType: MessageType.Error,
          position: Position.TopRight,
        })
    );

    this.dataSource = new MatTableDataSource<ListUser>(allUsers.listUsers);    
    this.paginator.length = allUsers.totalCount;
  }

  async pageChanged() {
    await this.getUsers();
  }

  async ngOnInit() {
    await this.getUsers();
  }

  assignRole(id: string){
    this.dialogService.openDialog({
      componentType: AuthorizeUserDialogComponent,
      data: id,
      options: {
        width: '750px',
      },
      afterClosed: () => {
        this.alertify.message("Roller güncellendi.",{
          messageType: MessageType.Success,
          position: Position.TopRight,
        })
      }
    });
  }
}
