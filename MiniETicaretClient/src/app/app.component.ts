import { Component } from '@angular/core';
import { AuthService } from './services/common/auth.service';
import {
  CustomToastrService,
  ToastrMessageType,
  ToastrPosition,
} from './services/ui/custom-toastr.service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {
  constructor(
    public authService: AuthService,
    private toastrService: CustomToastrService,
    private router: Router
  ) {
    authService.identityCheck();
  }

  signOut() {
    localStorage.removeItem('accessToken');
    this.authService.identityCheck();
    this.router.navigate([""]);    
    this.toastrService.message('Oturum Kapatıldı!', 'Oturum Kapatıldı', {
      messageType: ToastrMessageType.Info,
      position: ToastrPosition.TopRight,
    });
  }
}
