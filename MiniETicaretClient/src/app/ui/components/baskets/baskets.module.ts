import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketsComponent } from './baskets.component';
import { RouterModule } from '@angular/router';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

@NgModule({
  declarations: [BasketsComponent],
  exports: [BasketsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: '', component: BasketsComponent }]),
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class BasketsModule {}
