import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UpdatePasswordComponent } from './update-password.component';

@NgModule({
  declarations: [
    UpdatePasswordComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: '', component: UpdatePasswordComponent }]),
  ]
})
export class UpdatePasswordModule { }
