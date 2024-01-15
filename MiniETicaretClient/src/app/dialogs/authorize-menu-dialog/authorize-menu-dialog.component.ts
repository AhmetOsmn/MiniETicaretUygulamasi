import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';

@Component({
  selector: 'app-authorize-menu-dialog',
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrls: ['./authorize-menu-dialog.component.scss'],
})
export class AuthorizeMenuDialogComponent extends BaseDialog<AuthorizeMenuDialogComponent> {
  constructor(
    dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
    @Inject(MAT_DIALOG_DATA)
    public data: any
  ) {
    super(dialogRef);
  }

  addRole() {}
}

export enum AuthorizeMenuState {
  Yes,
  No,
}
