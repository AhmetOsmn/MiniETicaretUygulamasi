import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { roleController } from 'src/app/constants/api/api-controllers';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root',
})
export class RoleService {
  constructor(private httpClientService: HttpClientService) {}

  async getRoles(
    page: number,
    size: number,
    successCallback?: () => void,
    errorCallback?: (errorMessage: string) => void
  ) {
    const observable: Observable<any> = this.httpClientService.get({
      controller: roleController.controllerName,
      queryString: `page=${page}&size=${size}`,	
    });

    const promiseData = firstValueFrom(observable);

    promiseData.then(successCallback).catch(errorCallback);

    return await promiseData;
  }

  async create(
    name: string,
    successCallback?: () => void,
    errorCallback?: (errorMessage: string) => void
  ) {
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: roleController.controllerName,
      },
      { name: name }
    );

    const promiseData = firstValueFrom(observable);

    promiseData.then(successCallback).catch(errorCallback);

    return (await promiseData) as { succeeded: boolean };
  }
}
