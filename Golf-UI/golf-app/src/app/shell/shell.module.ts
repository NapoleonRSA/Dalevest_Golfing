import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ShellRoutingModule } from './shell-routing.module';
import { ShellNavlistComponent } from './shell-navlist/shell-navlist.component';
import { MaterialComponentsModule } from '../shared/material-components/material-components.module';

@NgModule({
  declarations: [
    ShellNavlistComponent],
  imports: [
    CommonModule,
    ShellRoutingModule,
    MaterialComponentsModule
  ]
})
export class ShellModule { }
