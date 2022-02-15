import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AdminComponent } from './admin/admin.component';
import { EmailTemplateComponent } from './admin/pages/email-template/email-template.component';
import { RecipientGroupComponent } from './admin/pages/recipient-group/recipient-group.component';
import { UserEmailTemplateComponent } from './admin/pages/user-email-template/user-email-template.component';
import { HeaderComponent } from './shared/header/header.component';
import { FooterComponent } from './shared/footer/footer.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { SideNavComponent } from './shared/side-nav/side-nav.component';
import { DashboardComponent } from './admin/pages/dashboard/dashboard.component';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ModelComponent } from './shared/model/model.component';
import { ModelService } from './core/services/modal/model.service';
import { MailSettingsComponent } from './admin/pages/mail-settings/mail-settings.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    AdminComponent,
    EmailTemplateComponent,
    RecipientGroupComponent,
    UserEmailTemplateComponent,
    HeaderComponent,
    FooterComponent,
    SideNavComponent,
    DashboardComponent,
    ModelComponent,
    MailSettingsComponent,    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(),
    CKEditorModule,
    FontAwesomeModule
  ],
  providers: [ModelService],
  bootstrap: [AppComponent],
  entryComponents: [ ModelComponent ],
})
export class AppModule { }
