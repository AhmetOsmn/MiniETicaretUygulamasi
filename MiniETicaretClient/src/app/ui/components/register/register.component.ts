import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  UntypedFormBuilder,
  UntypedFormGroup,
  ValidationErrors,
  Validators,
} from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent } from 'src/app/base/base.component';
import { Create_User } from 'src/app/contracts/users/create_user';
import { User } from 'src/app/entities/user';
import { UserService } from 'src/app/services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from 'src/app/services/ui/custom-toastr.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent extends BaseComponent implements OnInit {
  constructor(
    private formBuilder: UntypedFormBuilder,
    private userService: UserService,
    private toastrService: CustomToastrService,
    spinner: NgxSpinnerService
  ) {
    super(spinner)
  }

  frm: UntypedFormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group(
      {
        nameSurname: [
          '',
          [
            Validators.required,
            Validators.maxLength(50),
            Validators.minLength(2),
          ],
        ],
        userName: [
          '',
          [
            Validators.required,
            Validators.maxLength(50),
            Validators.minLength(2),
          ],
        ],
        email: [
          '',
          [Validators.required, Validators.maxLength(50), Validators.email],
        ],
        password: [
          '',
          [
            Validators.required,
            Validators.maxLength(50),
            Validators.minLength(5),
          ],
        ],
        passwordRetry: [
          '',
          [
            Validators.required,
            Validators.maxLength(50),
            Validators.minLength(5),
          ],
        ],
      },
      {
        validators: (group: AbstractControl): ValidationErrors | null => {
          let password = group.get('password').value;
          let passwordRetry = group.get('passwordRetry').value;
          return password === passwordRetry ? null : { notSame: true };
        },
      }
    );
  }

  get component() {
    return this.frm.controls;
  }

  submitted: boolean = false;

  async onSubmit(user: User) {
    this.submitted = true;
    
    if (this.frm.invalid) return;

    const result: Create_User = await this.userService.create(user);

    if(result.succeeded){
      this.toastrService.message(result.message, "Kullanıcı Kaydı Başarılı", {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      })
    }
    else{
      this.toastrService.message(result.message, "Hata", {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      })
    }
  }
}
