import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RecipientGroupComponent } from './recipient-group.component';

describe('RecipientGroupComponent', () => {
  let component: RecipientGroupComponent;
  let fixture: ComponentFixture<RecipientGroupComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RecipientGroupComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RecipientGroupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
