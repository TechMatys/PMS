import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

interface SentMailTemplate {
  subject: string,
  sentDate: Date;
  to: string;
  htmlContent: string;
}

interface DraftMailTemplate {
  subject: string,
  sentDate: Date;
  to: string;
  htmlContent: string;
}

@Component({
  selector: 'app-user-email-template',
  templateUrl: './user-email-template.component.html',
  styleUrls: ['./user-email-template.component.css']
})
export class UserEmailTemplateComponent implements OnInit {
  mailForm = new FormGroup({
    to: new FormControl(''),
    from: new FormControl(''),
    subject: new FormControl(''),
    htmlContent: new FormControl(''),
  });

  sentMaillist: SentMailTemplate[] = [];
  draftMaillist: DraftMailTemplate[] = [];
  public Editor = ClassicEditor;
  controllerName = "user-email-template";
  templatesList = [{
    templateId: 0, title: '-- Select Template --'
  }];
  templateContent: string = "";
  modalRef: any;

  constructor(private http: HttpService, private fB: FormBuilder, private notifyService: NotificationService,
    private modalService: BsModalService) {
    this.modalRef = BsModalRef;
  }

  sendMail() {
    this.http.create(this.controllerName + "/send-mail", this.mailForm.value)
      .subscribe({
        next: (res) => {
          this.notifyService.showSuccess("Mail sent successfully.", "Success");
        },
        error: (e) => console.error(e)
      });
  }

  getSentMail() {
    this.http.getAll(this.controllerName + "/sent-mail").subscribe(res => {
      this.sentMaillist = res;
    });
  }

  getDraftMail() {
    this.http.getAll(this.controllerName + "/draft-mail").subscribe(res => {
      this.draftMaillist = res;
    });
  }


  openModal(template: TemplateRef<any>) {
    this.templateContent = "";
    this.http.getAll("template").subscribe(res => {
      this.templatesList = res;
    });

    this.modalRef = this.modalService.show(template, { class: 'modal-lg' });
  }

  viewTemplate(id: any) {
    if (id > 0) {
      this.http.get("template", id).subscribe(res => {
        this.templateContent = res.htmlContent;
      });
    }
    else {
      this.templateContent = "";
    }
  }

  selectTemplate(content: string) {
    this.mailForm.controls['htmlContent'].setValue(content);
    this.modalRef.hide();
  }

  ngOnInit(): void {
    this.getSentMail();
    this.getDraftMail()
  }

}