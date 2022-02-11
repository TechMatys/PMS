import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService  } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';

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

  isShown: boolean = false;  
  isAddNew: boolean = true;
  templateId: number = 0;
  controllerName = "template";

  constructor(private http : HttpService, private fB : FormBuilder, private notifyService : NotificationService ) {
  }      

  getTemplate(){
    this.isShown = true;
    this.http.getAll(this.controllerName).subscribe(res => {
      this.templatelist = res;
    });
  }

  addTemplate(){
    this.isShown = false;   
    this.isAddNew = true;  
    const data = {
      title : '',
      description : '',
      htmlContent: ''
    }
    this.createTemplateForm(data);
  }
  
  saveTemplate(){
    this.templateData = this.templateForm.value;

    const data = Object.assign({}, this.templateData);

    this.http.create(this.controllerName,data)
      .subscribe({
        next: (res) => {
          this.getTemplate();
          // this.notifyService.showSuccess("Adding template", "Success")
        },
        error: (e) => console.error(e)
      });   
  }

  createTemplateForm(data : any){
    this.templateForm = this.fB.group({
      title : [data.title],
      description : [data.description],
      htmlContent : [data.htmlContent]
    });
  }
  
  editTemplate(id : any){  
    this.templateId = id;
    this.http.get(this.controllerName,id).subscribe(res => {      
      this.isShown = false;
      this.isAddNew = false;  
      this.createTemplateForm(res);
    });  
  }

  viewTemplate(id : any){      
    this.http.get(this.controllerName,id).subscribe(res => {      
     
    });  
  }

  updateTemplate(){
    this.templateData = this.templateForm.value;

    const data = Object.assign({}, this.templateData);

    this.http.update(this.controllerName,this.templateId, data)
      .subscribe({
        next: (res) => {
          this.getTemplate();
        },
        error: (e) => console.error(e)
      });  
  }

  deleteTemplate(id : any){
    this.http.delete(this.controllerName, id).subscribe(res => {
      this.getTemplate();
    });   
  }

  ngOnInit(): void {
    this.isShown = ! this.isShown;    
    this.getTemplate();
  }

}