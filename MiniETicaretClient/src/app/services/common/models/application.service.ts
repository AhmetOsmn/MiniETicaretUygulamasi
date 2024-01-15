import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { applicationServicesController } from 'src/app/constants/api/api-controllers';
import { Menu } from 'src/app/contracts/application-configurations/menu';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root',
})
export class ApplicationService {
  constructor(private httpClientService: HttpClientService) {}

  async getAuthorizeDefinitionEndpoints() {
    const observable: Observable<Menu[]> = this.httpClientService.get<Menu[]>({
      controller: applicationServicesController.controllerName,
    });

    return await firstValueFrom(observable);
  }
}
