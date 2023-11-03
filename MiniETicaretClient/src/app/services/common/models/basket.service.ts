import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { ListBasketItem } from 'src/app/contracts/basket/list_basket_item';
import { Observable, firstValueFrom } from 'rxjs';
import { basketController } from 'src/app/constants/api/api-controllers';
import { CreateBasketItem } from 'src/app/contracts/basket/create_basket_item';
import { UpdateQuantity } from 'src/app/contracts/basket/update_quantity';

@Injectable({
  providedIn: 'root',
})
export class BasketService {
  constructor(private httpClientService: HttpClientService) {}

  async getBasketItems(): Promise<ListBasketItem[]> {
    const observable: Observable<ListBasketItem[]> = this.httpClientService.get(
      {
        controller: basketController.controllerName,
        action: basketController.actions.getBasketItems,
      }
    );

    return await firstValueFrom(observable);
  }

  async addItemToBasket(basketItem: CreateBasketItem): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: basketController.controllerName,
        action: basketController.actions.addItemToBasket,
      },
      basketItem
    );

    return await firstValueFrom(observable);
  }

  async updateQuantity(basketItem: UpdateQuantity): Promise<void> {
    const observable: Observable<any> = this.httpClientService.put(
      {
        controller: basketController.controllerName,
        action: basketController.actions.updateQuantity,
      },
      basketItem
    );

    return await firstValueFrom(observable);
  }

  async removeBasketItem(basketItemId: string): Promise<ListBasketItem[]> {
    const observable: Observable<ListBasketItem[]> =
      this.httpClientService.delete(
        {
          controller: basketController.controllerName,
          action: basketController.actions.removeBasketItem,
        },
        basketItemId
      );

    return await firstValueFrom(observable);
  }
}
