import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { NgxSpinnerService } from 'ngx-spinner';
import { Observable } from 'rxjs';
import { SpinnerType } from 'src/app/base/base.component';
import { AuthService } from 'src/app/services/common/auth.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private jwtHelper: JwtHelperService,
    private router: Router,
    private toastrService: CustomToastrService,
    private spinnerService: NgxSpinnerService,
    private authService: AuthService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        
    this.spinnerService.show(SpinnerType.BallAtom);

    if (!this.authService.isAuthenticated) {
      this.router.navigate(["login"], {
        queryParams: { returnUrl: state.url }
      });

      this.toastrService.message("Oturum açmanız gerekiyor!", "Yetkisiz Erişim", {
        messageType: ToastrMessageType.Warning,
        position: ToastrPosition.TopRight
      })
    }

    this.spinnerService.hide(SpinnerType.BallAtom);

    return true;
  }
}
