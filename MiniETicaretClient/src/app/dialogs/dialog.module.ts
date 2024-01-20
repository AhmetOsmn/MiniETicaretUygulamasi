import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatListModule } from '@angular/material/list';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { FileUploadModule } from '../services/common/file-upload/file-upload.module';
import { AuthorizeMenuDialogComponent } from './authorize-menu-dialog/authorize-menu-dialog.component';
import { BasketItemRemoveDialogComponent } from './basket-item-remove-dialog/basket-item-remove-dialog.component';
import { CompleteOrderDialogComponent } from './complete-order-dialog/complete-order-dialog.component';
import { CompleteShoppingDialogComponent } from './complete-shopping-dialog/complete-shopping-dialog.component';
import { DeleteDialogComponent } from './delete-dialog/delete-dialog.component';
import { OrderDetailDialogComponent } from './order-detail-dialog/order-detail-dialog.component';
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';
import { AuthorizeUserDialogComponent } from './authorize-user-dialog/authorize-user-dialog.component';


@NgModule({
  declarations: [
    DeleteDialogComponent,
    SelectProductImageDialogComponent,
    BasketItemRemoveDialogComponent,
    CompleteShoppingDialogComponent,
    OrderDetailDialogComponent,
    CompleteOrderDialogComponent,
    AuthorizeMenuDialogComponent,
    AuthorizeUserDialogComponent,
  ],
  imports: [
    MatDialogModule,
    MatButtonModule,
    MatCardModule,
    MatTableModule,
    MatToolbarModule,
    MatBadgeModule,
    CommonModule,
    FileUploadModule,
    MatListModule
  ],
})
export class DialogModule {}
