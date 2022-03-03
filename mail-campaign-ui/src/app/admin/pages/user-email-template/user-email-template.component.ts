import { Component, OnInit, TemplateRef } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { faCalendarAlt } from '@fortawesome/free-solid-svg-icons';

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
    scheduleDate: new FormControl('')
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
  faCalendar = faCalendarAlt;
  today: Date;

  constructor(private http: HttpService, private fB: FormBuilder, private notifyService: NotificationService,
    private modalService: BsModalService) {
    this.modalRef = BsModalRef;
    this.today = new Date();
  }

  sendMail() {
    this.callSendMailAPI(1);
  }

  draftMail() {
    this.callSendMailAPI(2);
  }

  scheduleLaterMail(template: TemplateRef<any>) {    
    this.modalRef = this.modalService.show(template);
  }

  sendLaterMail() {
   this.callSendMailAPI(3);
  }

  callSendMailAPI(statusId: number) {
    var message = "", apiName = "";
    if (statusId === 2) {
      message = "Mail draft successfully."
      apiName = "/draft-mail";
    }
    else if (statusId === 3) {
      message = "Mail scheduled for later successfully."
      apiName = "/send-later";
    }
    else {
      message = "Mail sent successfully."
      apiName = "/send-mail";
    }
    this.http.create(this.controllerName + apiName, this.mailForm.value)
      .subscribe({
        next: (res) => {
          this.notifyService.showSuccess(message, "Success");
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

  previewMailTemplate() {
    
  }

  resetMail() {
    
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