import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EmailTemplateComponent } from './admin/pages/email-template/email-template.component';
import { RecipientGroupComponent } from './admin/pages/recipient-group/recipient-group.component';
import { HeaderComponent } from './shared/header/header.component';

const routes: Routes = [
  {
  path:'', component: HeaderComponent
  },
  {
    path:'Template', component: EmailTemplateComponent
  },
  {
    path: 'RecipientGroup', component: RecipientGroupComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
