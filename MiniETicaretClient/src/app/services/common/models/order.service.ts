import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateOrder } from 'src/app/contracts/order/create_order';
import { ordersController } from 'src/app/constants/api/api-controllers';
import { Observable, firstValueFrom } from 'rxjs';
import { ListOrder } from 'src/app/contracts/order/list_order';

@Injectable({
  providedIn: 'root',
})
export class OrderService {
  constructor(private httpClientService: HttpClientService) {}

  async createOrder(order: CreateOrder): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: ordersController.controllerName,
      },
      order
    );

    await firstValueFrom(observable);
  }

  async getAllOrders(
    page: number = 0,
    size: number = 5,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ): Promise<{ totalOrderCount: number; orders: ListOrder[] }> {
    const observable: Observable<{
      totalOrderCount: number;
      orders: ListOrder[];
    }> = this.httpClientService.get({
      controller: ordersController.controllerName,
      queryString: `page=${page}&size=${size}`,
    });

    const promiseData = firstValueFrom(observable);
    promiseData.then((value) => {successCallBack()})
    .catch((error) => {errorCallBack(error)});

    return await promiseData;
  }
}
