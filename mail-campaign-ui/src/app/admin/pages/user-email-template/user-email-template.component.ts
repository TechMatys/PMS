import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { faEdit, faTrash, faEye } from '@fortawesome/free-solid-svg-icons';
import { ModelService } from 'src/app/core/services/modal/model.service';

interface UserEmailTemplate {
  userEmailTemplateId: any,
  subject: string;
  createdDate: string;
}

@Component({
  selector: 'app-user-email-template',
  templateUrl: './user-email-template.component.html',
  styleUrls: ['./user-email-template.component.css']
})
export class UserEmailTemplateComponent implements OnInit {
  templateForm = new FormGroup({
    to: new FormControl(''),
    subject: new FormControl(''),
    htmlContent: new FormControl('')
  });

  templatelist: UserEmailTemplate[] = [];
  templateData: UserEmailTemplate[] = [];
  public Editor = ClassicEditor;
  faEdit = faEdit;
  faDelete = faTrash;
  faView = faEye;

  isShown: boolean = false;
  isAddNew: boolean = true;
  IsRecordFetching: boolean = false;
  controllerName = "user-email-template";

  constructor(private http: HttpService, private fB: FormBuilder, private notifyService: NotificationService,
    private modelService: ModelService) {
  }

  getTemplate() {
    this.isShown = true;
    this.IsRecordFetching = true;
    this.http.getAll(this.controllerName).subscribe(res => {
      this.templatelist = res;
      this.IsRecordFetching = false;
    });
  }

  showToaster() {
      this.notifyService.showSuccess("Adding template", "Success")
    this.modelService.confirm('Please confirm..', 'Do you really want to ... ?')
      .then((confirmed) => console.log('User confirmed:', confirmed))
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }

  addTemplate() {
    this.isShown = false;
    this.isAddNew = true;
    const data = {
      to: '',
      subject: '',
      htmlContent: ''
    }
    this.createTemplateForm(data);
  }

  saveTemplate() {
    this.templateData = this.templateForm.value;

    const data = Object.assign({}, this.templateData);

    this.http.create(this.controllerName, data)
      .subscribe({
        next: (res) => {
          this.notifyService.showSuccess("Template saved successfully.", "Success");
          this.getTemplate();
        },
        error: (e) => console.error(e)
      });
  }

  createTemplateForm(data: any) {
    this.templateForm = this.fB.group({
      to: [data.to],
      subject: [data.subject],
      htmlContent: [data.htmlContent]
    });
  }

  viewTemplate(id: any) {
    this.http.get(this.controllerName, id).subscribe(res => {
      const text = res.htmlContent;      
      this.modelService.confirm('Template Preview', text, 'Close', '', 'lg');
    });
  }

  deleteTemplate(id: any) {
    this.modelService.confirm('Confirmation', 'Are you sure you want to delete this template?', 'Yes', 'No', 'md')
      .then((confirmed) => {
        if (confirmed) {
          this.http.delete(this.controllerName, id).subscribe(res => {
            this.notifyService.showSuccess("Template deleted successfully.", "Success");
            this.getTemplate();
          });
        }
      });
  }

  ngOnInit(): void {
    this.isShown = !this.isShown;
    this.IsRecordFetching = !this.IsRecordFetching;
    this.getTemplate();
  }

}