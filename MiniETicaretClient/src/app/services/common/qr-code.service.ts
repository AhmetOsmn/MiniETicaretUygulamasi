import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { productsController } from 'src/app/constants/api/api-controllers';
import { HttpClientService } from './http-client.service';

@Injectable({
  providedIn: 'root',
})
export class QrCodeService {
  constructor(private httpClientService: HttpClientService) {}

  async generateQrCode(productId: string) {
    const observable: Observable<Blob> = this.httpClientService.get(
      {
        controller: productsController.controllerName,
        action: productsController.actions.generateQrCode,
        responseType: 'blob',
      },
      productId
    );

    return await firstValueFrom(observable)
  }
}
