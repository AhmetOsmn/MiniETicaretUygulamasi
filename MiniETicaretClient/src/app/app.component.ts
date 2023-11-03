import { Component } from '@angular/core';
import { AuthService } from './services/common/auth.service';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from './services/ui/custom-toastr.service';
import { Router } from '@angular/router';
import { SocialAuthService } from '@abacritt/angularx-social-login';
import { HttpClientService } from './services/common/http-client.service';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(
    public authService: AuthService,
    private toastrService: CustomToastrService,
    private router: Router,
    private socialAuthService: SocialAuthService,
    private httpClient: HttpClientService
  ) {

    httpClient.delete({
      controller: "baskets",
      action: "RemoveBasketItem"
    },"4621e1d3-2a1d-4a51-8226-a26b7cd6dd4b").subscribe(data => console.log("get data: ",data))


    authService.identityCheck();
  }

  signOut() {
    localStorage.removeItem('accessToken');
    this.socialAuthService.signOut();
    this.authService.identityCheck();
    this.router.navigate(['']);
    this.toastrService.message('Oturum Kapatıldı!', 'Oturum Kapatıldı', {
      messageType: ToastrMessageType.Info,
      position: ToastrPosition.TopRight,
    });
  }
}
