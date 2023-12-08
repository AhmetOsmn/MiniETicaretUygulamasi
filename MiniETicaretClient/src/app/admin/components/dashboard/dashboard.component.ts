import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { HubUrls } from 'src/app/constants/signalr/hub-urls';
import { ReceiveFunctionNames } from 'src/app/constants/signalr/receive-function-names';
import {
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { SignalRService } from 'src/app/services/common/signalr.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent extends BaseComponent implements OnInit {
  constructor(
    private alertify: AlertifyService,
    spinner: NgxSpinnerService,
    private signalRService: SignalRService
  ) {
    super(spinner);
  }

  ngOnInit(): void {
    this.signalRService.on(
      HubUrls.ProductsHub,
      ReceiveFunctionNames.ProductAddedMessage,
      (message) => {
        this.alertify.message(message, {
          messageType: MessageType.Notify,
          position: Position.TopRight,
        });
      }
    );
    this.signalRService.on(
      HubUrls.OrdersHub,
      ReceiveFunctionNames.OrderAddedMessage,
      (message) => {
        this.alertify.message(message, {
          messageType: MessageType.Notify,
          position: Position.TopCenter,
        });
      }
    );
  }
}
