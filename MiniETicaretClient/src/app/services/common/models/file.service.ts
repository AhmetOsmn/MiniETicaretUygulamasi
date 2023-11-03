import { Observable, firstValueFrom } from 'rxjs';
import { HttpClientService } from '../http-client.service';
import { Injectable } from '@angular/core';
import { BaseUrl } from 'src/app/contracts/files/base_url';
import { filesController } from 'src/app/constants/api/api-controllers';

@Injectable({
  providedIn: 'root',
})
export class FileService {
  constructor(private httpClientService: HttpClientService) {}

  async getBaseStorageUrl() : Promise<BaseUrl> {
    const getObservable: Observable<BaseUrl> =
      this.httpClientService.get<BaseUrl>({  
        controller: filesController.controllerName,        
        action: filesController.actions.getBaseStorageUrl,
      });

    return await firstValueFrom(getObservable);
  }
}
