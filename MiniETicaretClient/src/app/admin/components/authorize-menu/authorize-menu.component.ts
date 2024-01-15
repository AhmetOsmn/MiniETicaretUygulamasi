import { FlatTreeControl } from '@angular/cdk/tree';
import { Component, OnInit } from '@angular/core';
import {
  MatTreeFlatDataSource,
  MatTreeFlattener,
} from '@angular/material/tree';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { AuthorizeMenuDialogComponent } from 'src/app/dialogs/authorize-menu-dialog/authorize-menu-dialog.component';
import { DialogService } from 'src/app/services/common/dialog.service';
import { ApplicationService } from 'src/app/services/common/models/application.service';

interface ExampleFlatNode {
  expandable: boolean;
  name: string;
  level: number;
}

interface ITreeMenu {
  name?: string;
  actions?: ITreeMenu[];
  code?: string;
}

@Component({
  selector: 'app-authorize-menu',
  templateUrl: './authorize-menu.component.html',
  styleUrls: ['./authorize-menu.component.scss'],
})
export class AuthorizeMenuComponent extends BaseComponent implements OnInit {
  constructor(
    spinner: NgxSpinnerService,
    private applicationService: ApplicationService,
    private dialogService: DialogService
  ) {
    super(spinner);
  }

  async ngOnInit(): Promise<void> {
    this.dataSource.data = (
      await this.applicationService.getAuthorizeDefinitionEndpoints()
    ).map((m) => {
      const treeMenu: ITreeMenu = {
        name: m.name,
        actions: m.actions.map((a) => {
          const treeMenu: ITreeMenu = {
            name: a.definition,
            code: a.code,
          };
          return treeMenu;
        }),
      };
      return treeMenu;
    });
  }

  treeControl = new FlatTreeControl<ExampleFlatNode>(
    (node) => node.level,
    (node) => node.expandable
  );

  treeFlattener = new MatTreeFlattener(
    (menu: ITreeMenu, level: number) => {
      return {
        expandable: menu.actions?.length > 0,
        name: menu.name,
        level: level,
        code: menu.code,
      };
    },
    (node) => node.level,
    (node) => node.expandable,
    (node) => node.actions
  );

  dataSource = new MatTreeFlatDataSource(this.treeControl, this.treeFlattener);

  hasChild = (_: number, node: ExampleFlatNode) => node.expandable;

  assignRole(code: string, name: string){
    this.dialogService.openDialog({
      componentType: AuthorizeMenuDialogComponent,
      data: {code, name},
      options: {
        width: '750px',
      },
      afterClosed: () => {

      }
    })
  }
}
