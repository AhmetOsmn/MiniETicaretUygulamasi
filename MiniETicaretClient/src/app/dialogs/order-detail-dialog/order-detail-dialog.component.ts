import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { SingleOrder } from 'src/app/contracts/order/single_order';
import { DialogService } from 'src/app/services/common/dialog.service';
import { OrderService } from 'src/app/services/common/models/order.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';
import { BaseDialog } from '../base/base-dialog';
import {
  CompleteOrderDialogComponent,
  CompleteOrderState,
} from '../complete-order-dialog/complete-order-dialog.component';

@Component({
  selector: 'app-order-detail-dialog',
  templateUrl: './order-detail-dialog.component.html',
  styleUrls: ['./order-detail-dialog.component.scss'],
})
export class OrderDetailDialogComponent
  extends BaseDialog<OrderDetailDialogComponent>
  implements OnInit
{
  constructor(
    dialogRef: MatDialogRef<OrderDetailDialogComponent>,
    private orderService: OrderService,
    private dialogService: DialogService,
    private spinnerService: NgxSpinnerService,
    private toastrService: CustomToastrService,
    @Inject(MAT_DIALOG_DATA) public data: OrderDetailDialogState | string
  ) {
    super(dialogRef);
  }

  singleOrder: SingleOrder;
  totalPrice: number;
  displayedColumns: string[] = [
    'productName',
    'unitPrice',
    'quantity',
    'totalPrice',
  ];
  dataSource = [];
  clickedRows = new Set<any>();

  async ngOnInit(): Promise<void> {
    this.singleOrder = await this.orderService.getOrderById(
      this.data as string
    );
    this.dataSource = this.singleOrder.basketItems;
    this.totalPrice = this.singleOrder.basketItems
      .map((basketItem, index) => basketItem.unitPrice * basketItem.quantity)
      .reduce((a, b) => a + b, 0);
  }

  completeOrder() {
    this.dialogService.openDialog({
      componentType: CompleteOrderDialogComponent,
      data: CompleteOrderState.Yes,
      afterClosed: async () => {
        this.spinnerService.show(SpinnerType.BallAtom);
        await this.orderService.completeOrder(this.data as string);
        this.spinnerService.hide(SpinnerType.BallAtom);
        this.toastrService.message("Sipariş başarıyla tamamlanmıştır! Müşteriye bilgi verildi.", "Sipariş Tamamlandı", {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        })
      },
    });
  }
}

export enum OrderDetailDialogState {
  Close,
  CompleteOrder,
}
