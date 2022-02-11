import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UserEmailTemplateComponent } from './user-email-template.component';

describe('UserEmailTemplateComponent', () => {
  let component: UserEmailTemplateComponent;
  let fixture: ComponentFixture<UserEmailTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UserEmailTemplateComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UserEmailTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
