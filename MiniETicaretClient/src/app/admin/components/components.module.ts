import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { DashboardModule } from './dashboard/dashboard.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    DashboardModule,
  ]

})
export class ComponentsModule { }
