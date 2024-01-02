import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import {
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { UserService } from 'src/app/services/common/models/user.service';

@Component({
  selector: 'app-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss'],
})
export class UpdatePasswordComponent extends BaseComponent {
  constructor(
    spinnerService: NgxSpinnerService,
    private authService: UserAuthService,
    private activatedRoute: ActivatedRoute,
    private alertifyService: AlertifyService,
    private userService: UserService,
    private router: Router
  ) {
    super(spinnerService);
  }

  state: any;

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom);
    this.activatedRoute.params.subscribe({
      next: async (params) => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        this.state = await this.authService.verifyResetToken(
          resetToken,
          userId,
          () => {
            this.hideSpinner(SpinnerType.BallAtom);
          }
        );
      },
    });
  }

  async updatePassword(password: string, passwordConfirm: string) {
    this.showSpinner(SpinnerType.BallAtom);

    if (password !== passwordConfirm) {
      this.alertifyService.message('Şifreleri doğrulayınız!', {
        messageType: MessageType.Error,
        position: Position.TopRight,
      });
      this.hideSpinner(SpinnerType.BallAtom);
      return;
    }

    this.activatedRoute.params.subscribe({
      next: async (params) => {
        const userId: string = params['userId'];
        const resetToken: string = params['resetToken'];
        await this.userService.updatePassword(
          userId,
          resetToken,
          password,
          passwordConfirm,
          () => {
            this.alertifyService.message('Şifre başarıyla güncellenmiştir.', {
              messageType: MessageType.Success,
              position: Position.TopRight,
            });
            this.router.navigate(['/login']);
          },
          (error) => {
            console.log(error);
          }
        );
        this.hideSpinner(SpinnerType.BallAtom);
      },
    });
  }
}
