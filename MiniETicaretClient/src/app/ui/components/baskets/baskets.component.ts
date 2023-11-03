import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { ListBasketItem } from 'src/app/contracts/basket/list_basket_item';
import { UpdateQuantity } from 'src/app/contracts/basket/update_quantity';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { faShoppingBasket } from '@fortawesome/free-solid-svg-icons';

declare var $: any;

@Component({
  selector: 'app-baskets',
  templateUrl: './baskets.component.html',
  styleUrls: ['./baskets.component.scss'],
})
export class BasketsComponent extends BaseComponent implements OnInit {
  faTrashRestore = faShoppingBasket;

  constructor(
    spinner: NgxSpinnerService,
    private basketService: BasketService
  ) {
    super(spinner);
  }

  basketItems: ListBasketItem[];
  async ngOnInit() {
    this.showSpinner(SpinnerType.BallAtom);
    this.basketItems = await this.basketService.getBasketItems();
    this.hideSpinner(SpinnerType.BallAtom);
  }

  async changeQuantity(object: any) {
    this.showSpinner(SpinnerType.BallAtom);
    const basketItemId: string = object.target.attributes['id'].value;
    const quantity: number = object.target.value;
    let basketItem: UpdateQuantity = new UpdateQuantity();
    basketItem.basketItemId = basketItemId;
    basketItem.quantity = quantity;

    await this.basketService.updateQuantity(basketItem);
    this.hideSpinner(SpinnerType.BallAtom);
  }

  async removeBasketItem(basketItemId: string) {
    this.showSpinner(SpinnerType.BallAtom);
    await this.basketService.removeBasketItem(basketItemId);
    $('.' + basketItemId).fadeOut(500, () =>
      this.hideSpinner(SpinnerType.BallAtom)
    );
  }
}
