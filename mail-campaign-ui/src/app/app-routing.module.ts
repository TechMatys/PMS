import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './admin/pages/dashboard/dashboard.component';
import { EmailTemplateComponent } from './admin/pages/email-template/email-template.component';
import { RecipientGroupComponent } from './admin/pages/recipient-group/recipient-group.component';
import { UserEmailTemplateComponent } from './admin/pages/user-email-template/user-email-template.component';

const routes: Routes = [
   {
    path:'', component: DashboardComponent
  },
  {
    path:'Template', component: EmailTemplateComponent
  },
  {
    path:'RecipientGroup', component: RecipientGroupComponent
  },
  {
    path: 'UserEmailTemplate', component: UserEmailTemplateComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
