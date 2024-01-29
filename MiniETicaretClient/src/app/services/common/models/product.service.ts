import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { productsController } from 'src/app/constants/api/api-controllers';
import { Create_Product } from 'src/app/contracts/create_product';
import { List_Product } from 'src/app/contracts/list_product';
import { List_Product_Image } from 'src/app/contracts/list_product_image';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private httpClientService: HttpClientService) {}

  create(
    product: Create_Product,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ) {
    this.httpClientService
      .post(
        {
          controller: productsController.controllerName,
        },
        product
      )
      .subscribe(
        (result) => {
          successCallBack();
        },
        (errorResponse: HttpErrorResponse) => {
          const _error: Array<{ key: string; value: Array<String> }> =
            errorResponse.error;
          let message = '';
          _error.forEach((v, index) => {
            v.value.forEach((_v, _index) => {
              message += `${_v}<br>`;
            });
          });
          errorCallBack(message);
        }
      );
  }

  async read(
    page: number = 0,
    size: number = 5,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ): Promise<{ totalCount: number; products: List_Product[] }> {
    const promiseData: Promise<{
      totalCount: number;
      products: List_Product[];
    }> = this.httpClientService
      .get<{ totalCount: number; products: List_Product[] }>({
        controller: productsController.controllerName,
        queryString: `page=${page}&size=${size}`,
      })
      .toPromise();

    promiseData
      .then((d) => successCallBack())
      .catch((errorResponse: HttpErrorResponse) =>
        errorCallBack(errorResponse.message)
      );

    return await promiseData;
  }

  async delete(id: string) {
    const deleteObservable: Observable<any> =
      this.httpClientService.delete<any>(
        {
          controller: productsController.controllerName,
        },
        id
      );

    await firstValueFrom(deleteObservable);
  }

  async readImages(
    id: string,
    successCallBack?: () => void
  ): Promise<List_Product_Image[]> {
    const getObservable: Observable<List_Product_Image[]> =
      this.httpClientService.get<List_Product_Image[]>(
        {
          controller: productsController.controllerName,
          action: productsController.actions.getProductImages,
        },
        id
      );

    const images: List_Product_Image[] = await firstValueFrom(getObservable);
    successCallBack();
    return images;
  }

  async deleteImage(
    productId: string,
    imageId: string,
    successCallBack?: () => void
  ) {
    const deleteObservable = this.httpClientService.delete(
      {
        controller: productsController.controllerName,
        action: productsController.actions.deleteProductImage,
        queryString: `imageId=${imageId}`,
      },
      productId
    );

    await firstValueFrom(deleteObservable);
    successCallBack();
  }

  async changeImageShowcase(
    imageId: string,
    productId: string,
    successCallBack?: () => void
  ): Promise<void> {
    const changeImageShowcaseObservable = this.httpClientService.get({
      controller: productsController.controllerName,
      action: productsController.actions.changeShowcase,      
      queryString: `imageId=${imageId}&productId=${productId}`,
    });
    console.log(
      '🚀 ~ file: product.service.ts:129 ~ ProductService ~ productId:',
      productId
    );
    console.log(
      '🚀 ~ file: product.service.ts:129 ~ ProductService ~ imageId:',
      imageId
    );

    await firstValueFrom(changeImageShowcaseObservable);
    successCallBack();
  }

  async updateStockWithQrCode(productId: string, stock: number, successCallBack?: () => void): Promise<void> {
    const observable = this.httpClientService.put({
      controller: productsController.controllerName,
      action: productsController.actions.updateStockWithQrCode      
    },{productId, stock});

    await firstValueFrom(observable);
    successCallBack();
  }
}
