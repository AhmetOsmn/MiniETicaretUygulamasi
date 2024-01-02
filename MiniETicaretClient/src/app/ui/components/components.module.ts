import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';
import { PasswordResetModule } from './password-reset/password-reset.module';
import { ProductsModule } from './products/products.module';
import { RegisterModule } from './register/register.module';
import { UpdatePasswordModule } from './update-password/update-password.module';

@NgModule({
  declarations: [
  ],
  exports: [BasketsModule],
  imports: [
    CommonModule,
    ProductsModule,
    BasketsModule,
    HomeModule,
    RegisterModule,
    // LoginModule
    PasswordResetModule,
    UpdatePasswordModule
  ],
})
export class ComponentsModule {}
