import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateOrder } from 'src/app/contracts/order/create_order';
import { ordersController } from 'src/app/constants/api/api-controllers';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private httpClientService: HttpClientService) {}

  async createOrder(order: CreateOrder) {
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: ordersController.controllerName,
      },
      order
    );

    await firstValueFrom(observable);
  }
}
