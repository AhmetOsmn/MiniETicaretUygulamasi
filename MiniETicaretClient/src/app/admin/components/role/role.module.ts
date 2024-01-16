import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatTableModule } from '@angular/material/table';
import { RouterModule } from '@angular/router';
import { DeleteDirectiveModule } from 'src/app/directives/admin/delete.directive.module';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { RoleComponent } from './role.component';

@NgModule({
  declarations: [
    RoleComponent,
    CreateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: "", component: RoleComponent}
    ]),
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatTableModule,
    MatPaginatorModule,
    DeleteDirectiveModule
  ]
})
export class RoleModule { }
