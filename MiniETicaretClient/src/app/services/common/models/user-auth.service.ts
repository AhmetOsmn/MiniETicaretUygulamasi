import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from '../../ui/custom-toastr.service';
import { Observable, firstValueFrom } from 'rxjs';
import { LoginResponse } from 'src/app/contracts/users/login_response';
import { SocialUser } from '@abacritt/angularx-social-login';

@Injectable({
  providedIn: 'root',
})
export class UserAuthService {
  constructor(
    private httpClientService: HttpClientService,
    private toastrService: CustomToastrService
  ) {}

  async login(
    usernameOrEmail: string,
    password: string,
    callBackFunction?: () => void
  ): Promise<any> {
    const observable: Observable<any | LoginResponse> =
      this.httpClientService.post<any | LoginResponse>(
        {
          controller: 'auth',
          action: 'login',
        },
        { usernameOrEmail, password }
      );

    const loginResponse: LoginResponse = (await firstValueFrom(
      observable
    )) as LoginResponse;

    if (loginResponse.token) {
      localStorage.setItem('accessToken', loginResponse.token.accessToken);
      localStorage.setItem('refreshToken', loginResponse.token.refreshToken);

      this.toastrService.message(
        'Kullanıcı girişi başarılı.',
        'Giriş Başarılı',
        {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight,
        }
      );
    }

    callBackFunction();
  }

  async refreshTokenLogin(refreshToken: string, callBackFunction?: () => void) : Promise<any> {
    const observable: Observable<any | LoginResponse> = this.httpClientService.post({
      action: "refreshtokenlogin",
      controller: "auth"
    }, { refreshToken: refreshToken });

    const tokenResponse: LoginResponse = await firstValueFrom(observable) as LoginResponse;

    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);
    }

    callBackFunction();
  }

  async googleLogin(
    user: SocialUser,
    callBackFunction?: () => void
  ): Promise<any> {
    const observable: Observable<SocialUser | LoginResponse> =
      this.httpClientService.post<SocialUser | LoginResponse>(
        {
          action: 'google-login',
          controller: 'auth',
        },
        user
      );

    const loginResponse: LoginResponse = (await firstValueFrom(
      observable
    )) as LoginResponse;

    if (loginResponse) {
      localStorage.setItem('accessToken', loginResponse.token.accessToken);
      localStorage.setItem('refreshToken', loginResponse.token.refreshToken);

      this.toastrService.message(
        'Google ile giriş başarılı.',
        'Giriş Başarılı',
        {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight,
        }
      );
    }

    callBackFunction();
  }

  async facebookLogin(
    user: SocialUser,
    callBackFunction?: () => void
  ): Promise<any> {
    console.log('facebooklogin');
    const observable: Observable<SocialUser | LoginResponse> =
      this.httpClientService.post<SocialUser | LoginResponse>(
        {
          action: 'facebook-login',
          controller: 'auth',
        },
        user
      );

    const loginResponse: LoginResponse = (await firstValueFrom(
      observable
    )) as LoginResponse;

    if (loginResponse) {
      localStorage.setItem('accessToken', loginResponse.token.accessToken);
      localStorage.setItem('refreshToken', loginResponse.token.refreshToken);

      this.toastrService.message(
        'Facebook ile giriş başarılı.',
        'Giriş Başarılı',
        {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight,
        }
      );
    }

    callBackFunction();
  }
}
