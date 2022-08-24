/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { ConvertComponent } from './convert.component';

describe('ConvertComponent', () => {
  let component: ConvertComponent;
  let fixture: ComponentFixture<ConvertComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConvertComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConvertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
