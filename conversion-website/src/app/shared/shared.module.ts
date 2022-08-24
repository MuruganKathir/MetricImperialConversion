import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
    declarations: [
    ],
    imports: [
      CommonModule,
      RouterModule,
      FormsModule,
      ReactiveFormsModule,
      NgbModule
    ],
    exports: [
      // Modules
      NgbModule,
      FormsModule,
      ReactiveFormsModule,
    ],
  })
  export class SharedModule {}
