import { Injectable } from '@angular/core';
import { Observable, firstValueFrom } from 'rxjs';
import { usersController } from 'src/app/constants/api/api-controllers';
import { Create_User } from 'src/app/contracts/users/create_user';
import { ListUser } from 'src/app/contracts/users/list_user';
import { User } from 'src/app/entities/user';
import { HttpClientService } from '../http-client.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private httpClientService: HttpClientService) {}

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> =
      this.httpClientService.post<Create_User | User>(
        {
          controller: usersController.controllerName,
        },
        user
      );

    return (await firstValueFrom(observable)) as Create_User;
  }

  async updatePassword(
    userId: string,
    resetToken: string,
    password: string,
    passwordConfirm: string,
    successCallBack?: () => void,
    errorCallBack?: (error) => void
  ) {
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: usersController.controllerName,
        action: usersController.actions.updatePassword,
      },
      {
        userId: userId,
        resetToken: resetToken,
        newPassword: password,
        passwordConfirm: passwordConfirm,
      }
    );

    const promiseData: Promise<any> = firstValueFrom(observable);
    promiseData
      .then((value) => successCallBack())
      .catch((error) => errorCallBack(error));
    await promiseData;
  }

  async getAllUsers(
    page: number = 0,
    size: number = 5,
    successCallBack?: () => void,
    errorCallBack?: (errorMessage: string) => void
  ): Promise<{ totalCount: number; listUsers: ListUser[] }> {
    const observable: Observable<{
      totalCount: number;
      listUsers: ListUser[];
    }> = this.httpClientService.get({
      controller: usersController.controllerName,
      queryString: `page=${page}&size=${size}`,
    });

    const promiseData = firstValueFrom(observable);
    promiseData
      .then((value) => {
        if (successCallBack) successCallBack();
      })
      .catch((error) => {
        if (errorCallBack) errorCallBack(error);
      });

    return await promiseData;
  }

  async assignRoleToUser(id: string, roles: string[], successCallBack?:()=> void, errorCallBack?: (errorMessage: string) => void){
    const observable: Observable<any> = this.httpClientService.post(
      {
        controller: usersController.controllerName,
        action: usersController.actions.assignRoleToUser,
      },
      {
        id: id,
        roles: roles
      }
    );

    const promiseData: Promise<any> = firstValueFrom(observable);
    promiseData
      .then((value) => successCallBack())
      .catch((error) => errorCallBack(error));
    await promiseData;
  }
}
