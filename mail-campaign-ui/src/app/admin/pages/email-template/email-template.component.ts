import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { faEdit, faTrash, faEye } from '@fortawesome/free-solid-svg-icons';
import { ModelService } from 'src/app/core/services/modal/model.service';

interface Template {
  templateId: any,
  title: string;
  createdDate: string;
}

@Component({
  selector: 'app-email-template',
  templateUrl: './email-template.component.html',
  styleUrls: ['./email-template.component.css']
})
export class EmailTemplateComponent implements OnInit {
  templateForm = new FormGroup({
    title: new FormControl(''),
    description: new FormControl(''),
    htmlContent: new FormControl('')
  });

  templatelist: Template[] = [];
  templateData: Template[] = [];
  public Editor = ClassicEditor;
  faEdit = faEdit;
  faDelete = faTrash;
  faView = faEye;

  isShown: boolean = false;
  isAddNew: boolean = true;
  IsRecordFetching: boolean = false;
  templateId: number = 0;
  controllerName = "template";

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
    // this.notifyService.showSuccess("Adding template", "Success")
    this.modelService.confirm('Please confirm..', 'Do you really want to ... ?')
      .then((confirmed) => console.log('User confirmed:', confirmed))
      .catch(() => console.log('User dismissed the dialog (e.g., by using ESC, clicking the cross icon, or clicking outside the dialog)'));
  }

  addTemplate() {
    this.isShown = false;
    this.isAddNew = true;
    const data = {
      title: '',
      description: '',
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
          this.getTemplate();
        },
        error: (e) => console.error(e)
      });
  }

  createTemplateForm(data: any) {
    this.templateForm = this.fB.group({
      title: [data.title],
      description: [data.description],
      htmlContent: [data.htmlContent]
    });
  }

  editTemplate(id: any) {
    this.templateId = id;
    this.http.get(this.controllerName, id).subscribe(res => {
      this.isShown = false;
      this.isAddNew = false;
      this.createTemplateForm(res);
    });
  }

  viewTemplate(id: any) {
    this.http.get(this.controllerName, id).subscribe(res => {
      const text = res.htmlContent;
      this.modelService.confirm('Template', text, 'OK', 'No', 'md')
        .then((confirmed) => {
          if (confirmed) {
            this.http.delete(this.controllerName, id).subscribe(res => {
              this.getTemplate();
            });
          }
        });
    });
  }

  updateTemplate() {
    this.templateData = this.templateForm.value;

    const data = Object.assign({}, this.templateData);

    this.http.update(this.controllerName, this.templateId, data)
      .subscribe({
        next: (res) => {
          this.getTemplate();
        },
        error: (e) => console.error(e)
      });
  }

  deleteTemplate(id: any) {
    this.modelService.confirm('Confirmation', 'Are you sure you want to delete this template?', 'Yes', 'No', 'md')
      .then((confirmed) => {
        if (confirmed) {
          this.http.delete(this.controllerName, id).subscribe(res => {
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