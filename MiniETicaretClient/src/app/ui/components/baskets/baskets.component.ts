import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { ListBasketItem } from 'src/app/contracts/basket/list_basket_item';
import { UpdateQuantity } from 'src/app/contracts/basket/update_quantity';
import { BasketService } from 'src/app/services/common/models/basket.service';
import { faShoppingBasket } from '@fortawesome/free-solid-svg-icons';
import { OrderService } from 'src/app/services/common/models/order.service';
import { CreateOrder } from 'src/app/contracts/order/create_order';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from 'src/app/services/ui/custom-toastr.service';
import { Router } from '@angular/router';

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
    private basketService: BasketService,
    private orderService: OrderService,
    private toastrService: CustomToastrService,
    private router: Router
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

  async completeShopping() {
    this.showSpinner(SpinnerType.BallAtom);
    const order: CreateOrder = new CreateOrder();
    order.address = 'Sarıyer';
    order.description = 'İlk siparişim.';
    await this.orderService.createOrder(order);
    this.hideSpinner(SpinnerType.BallAtom);
    this.toastrService.message('Sipariş alınmıştır!', 'Sipariş Oluşturuldu!', {
      messageType: ToastrMessageType.Info,
      position: ToastrPosition.TopRight,
    });
    this.router.navigate(["/"])
  }
}
