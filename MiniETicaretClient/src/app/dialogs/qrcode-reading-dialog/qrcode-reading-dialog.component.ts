import {
  Component,
  ElementRef,
  Inject,
  OnDestroy,
  OnInit,
  ViewChild,
} from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { NgxScannerQrcodeComponent } from 'ngx-scanner-qrcode';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { ProductService } from 'src/app/services/common/models/product.service';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from 'src/app/services/ui/custom-toastr.service';
import { BaseDialog } from '../base/base-dialog';

declare var $: any;

@Component({
  selector: 'app-qrcode-reading-dialog',
  templateUrl: './qrcode-reading-dialog.component.html',
  styleUrls: ['./qrcode-reading-dialog.component.scss'],
})
export class QrcodeReadingDialogComponent
  extends BaseDialog<QrcodeReadingDialogComponent>
  implements OnInit, OnDestroy
{
  constructor(
    dialogRef: MatDialogRef<QrcodeReadingDialogComponent>,
    private spinnerService: NgxSpinnerService,
    private toastrService: CustomToastrService,
    private productService: ProductService,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {
    super(dialogRef);
  }

  @ViewChild('scanner', { static: true }) scanner: NgxScannerQrcodeComponent;
  @ViewChild('txtStock', { static: true }) txtStock: ElementRef;

  ngOnInit(): void {
    this.scanner.start();
  }

  ngOnDestroy(): void {
    this.scanner.stop();
  }

  onEvent(event) {
    this.spinnerService.show(SpinnerType.BallAtom);
    const data = (event as { data: string }).data;

    if (data) {
      const jsonData = JSON.parse(data);
      const stockValue = (this.txtStock.nativeElement as HTMLInputElement)
        .value;

      this.productService.updateStockWithQrCode(
        jsonData.Id,
        parseInt(stockValue),
        () => {
          $('#btnClose').click();
          this.spinnerService.hide(SpinnerType.BallAtom);
          this.toastrService.message(
            `${jsonData.Name} ürününün stok değeri ${stockValue} olarak güncellendi.`,
            'Stok Başarıyla Güncellendi',
            {
              messageType: ToastrMessageType.Success,
              position: ToastrPosition.TopRight,
            }
          );
        }
      );
    }
  }
}
