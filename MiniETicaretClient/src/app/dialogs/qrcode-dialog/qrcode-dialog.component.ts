import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerType } from 'src/app/base/base.component';
import { QrCodeService } from 'src/app/services/common/qr-code.service';
import { BaseDialog } from '../base/base-dialog';

@Component({
  selector: 'app-qrcode-dialog',
  templateUrl: './qrcode-dialog.component.html',
  styleUrls: ['./qrcode-dialog.component.scss'],
})
export class QrcodeDialogComponent
  extends BaseDialog<QrcodeDialogComponent>
  implements OnInit
{
  constructor(
    dialogRef: MatDialogRef<QrcodeDialogComponent>,
    private spinnerService: NgxSpinnerService,
    private qrCodeService : QrCodeService,
    private domSanitizer : DomSanitizer,
    @Inject(MAT_DIALOG_DATA) public data: string
  ) {
    super(dialogRef);
  }

  qrCodeSafeUrl : SafeUrl;

  async ngOnInit(): Promise<void> {
    this.spinnerService.show(SpinnerType.BallAtom);
    const qrCodeBlob : Blob = await this.qrCodeService.generateQrCode(this.data);
    const qrCodeUrl : string = URL.createObjectURL(qrCodeBlob);
    this.qrCodeSafeUrl = this.domSanitizer.bypassSecurityTrustUrl(qrCodeUrl);
    this.spinnerService.hide(SpinnerType.BallAtom);
  }
}
