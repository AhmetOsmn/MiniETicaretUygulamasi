import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { User } from 'src/app/entities/user';
import { Create_User } from 'src/app/contracts/users/create_user';
import { Observable, firstValueFrom } from 'rxjs';
import { Login } from 'src/app/contracts/users/login';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from '../../ui/custom-toastr.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private httpClientService: HttpClientService,
    private toastrService: CustomToastrService
  ) {}

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> =
      this.httpClientService.post<Create_User | User>(
        {
          controller: 'users',
        },
        user
      );

    return (await firstValueFrom(observable)) as Create_User;
  }

  async login(
    usernameOrEmail: string,
    password: string,
    callBackFunction?: () => void
  ): Promise<any> {
    const observable: Observable<any | Login> = this.httpClientService.post<
      any | Login
    >(
      {
        controller: 'users',
        action: 'login',
      },
      { usernameOrEmail, password }
    );

    const loginResult: Login = (await firstValueFrom(observable)) as Login;

    if (loginResult.token) {

      localStorage.setItem("accessToken", loginResult.token.accessToken);
      localStorage.setItem("expiration", loginResult.token.expiration.toString());

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
}
