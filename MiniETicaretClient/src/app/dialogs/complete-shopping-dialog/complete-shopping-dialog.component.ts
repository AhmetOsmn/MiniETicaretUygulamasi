import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { BaseDialog } from '../base/base-dialog';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

declare var $: any;

@Component({
  selector: 'app-complete-shopping-dialog',
  templateUrl: './complete-shopping-dialog.component.html',
  styleUrls: ['./complete-shopping-dialog.component.scss'],
})
export class CompleteShoppingDialogComponent
  extends BaseDialog<CompleteShoppingDialogComponent>
  implements OnDestroy
{
  constructor(
    dialogRef: MatDialogRef<CompleteShoppingDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CompleteShoppingState
  ) {
    super(dialogRef);
  }

  show: boolean = false;

  ngOnDestroy(): void {
    if (!this.show) $('#basketModal').modal('show');
  }

  complete() {
    this.show = true;
  }
}

export enum CompleteShoppingState {
  Yes,
  No,
}
