import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Product } from 'src/app/contracts/product';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss'],
})
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor(spinner: NgxSpinnerService, private httpClientService: HttpClientService) {
    super(spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallSpinClockwiseFadeRotating);


    // get 
    this.httpClientService.get<Product[]>({
      controller: "products"
    }).subscribe(data => console.log(data))

    //post 
    // this.httpClientService.post({
    //   controller: "products",
    // }, {
    //   name: "cetvel",
    //   stock: 100,
    //   price: 15
    // }).subscribe();

    //put 
    // this.httpClientService.put({
    //   controller: "products",
    // }, {
    //   id: "ee7f82a7-d02a-4eb8-b677-023022ac2642",
    //   name: "Renkli kagit",
    //   stock: 1500,
    //   price: 5.5
    // }).subscribe();

    //delete 
    // this.httpClientService.delete({
    //   controller: "products"
    // }, "05e99db4-9754-49c3-879b-e01f759aa3d6").subscribe();

  }
}
