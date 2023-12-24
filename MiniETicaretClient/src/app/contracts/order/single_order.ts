export class SingleOrder {
  id: string;
  address: string;
  basketItems: BasketItem[];
  createdDate: Date;
  description: string;
  orderCode: string;
}

export class BasketItem {
  productName: string;
  quantity: number;
  unitPrice: number;
}
