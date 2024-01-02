import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { ComponentsModule } from './components/components.module';

@NgModule({
  declarations: [],
  exports : [ComponentsModule],
  imports: [CommonModule, ComponentsModule],
})
export class UiModule {}
