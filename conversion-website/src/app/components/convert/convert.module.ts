import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SharedModule } from 'src/app/shared/shared.module';
import { ConvertRoutingModule } from './convert-routing.module';

import { ConvertComponent } from './convert.component';

@NgModule({
    declarations: [ConvertComponent],
    imports: [CommonModule, SharedModule, ConvertRoutingModule, RouterModule],
  })
  export class ConvertModule {}
