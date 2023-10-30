import {
  FacebookLoginProvider,
  GoogleLoginProvider,
  SocialAuthService,
  SocialUser,
} from '@abacritt/angularx-social-login';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from '../../../base/base.component';
import { UserService } from 'src/app/services/common/models/user.service';
import { AuthService } from 'src/app/services/common/auth.service';
import { faFacebook } from '@fortawesome/free-brands-svg-icons';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from 'src/app/services/ui/custom-toastr.service';
import { UserAuthService } from 'src/app/services/common/models/user-auth.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent extends BaseComponent implements OnInit {
  faFacebook = faFacebook;
  constructor(
    spinner: NgxSpinnerService,
    private userAuthService: UserAuthService,
    private authService: AuthService,
    private socialAuthService: SocialAuthService,
    private toastrService: CustomToastrService,
    private activatedRoute: ActivatedRoute,
    private router: Router
  ) {
    super(spinner);
    socialAuthService.authState.subscribe(async (user: SocialUser) => {
      this.showSpinner(SpinnerType.BallAtom);
      switch (user.provider) {
        case 'GOOGLE':
          await userAuthService.googleLogin(user, () => {
            this.authService.identityCheck();
            this.hideSpinner(SpinnerType.BallAtom);
          });
          break;
        case 'FACEBOOK':
          await userAuthService.facebookLogin(user, () => {
            this.authService.identityCheck();
            this.hideSpinner(SpinnerType.BallAtom);
          });
      }
    });
  }

  ngOnInit(): void {}

  async login(usernameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.BallAtom);
    await this.userAuthService.login(usernameOrEmail, password, () => {
      this.authService.identityCheck();

      this.activatedRoute.queryParams.subscribe((params) => {
        const returnUrl: string = params['returnUrl'];
        if (returnUrl) this.router.navigate([returnUrl]);
      });
      this.hideSpinner(SpinnerType.BallAtom);
    });
  }

  facebookLogin() {
    this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
  }

  facebookLoginInfo() {
    this.toastrService.message(
      'Facebook login şuan kullanılamıyor.',
      'Facebook Login',
      {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopCenter,
      }
    );
  }
}
