import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { AuthorizeMenuModule } from './authorize-menu/authorize-menu.module';
import { CustomerModule } from './customers/customer.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { OrdersModule } from './orders/orders.module';
import { ProductsModule } from './products/products.module';
import { RoleModule } from './role/role.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    DashboardModule,
    OrdersModule,
    CustomerModule,
    DashboardModule,
    AuthorizeMenuModule,
    RoleModule
  ]

})
export class ComponentsModule { }
