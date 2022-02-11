import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService  } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';

interface RecipientGroup {
  recipientGroupId: any,
  groupName: string;
  createdDate: string;
}

interface Recipient {
  recipientId: any,
  emailAddress: string;
}

@Component({
  selector: 'app-recipient-group',
  templateUrl: './recipient-group.component.html',
  styleUrls: ['./recipient-group.component.css']
})
export class RecipientGroupComponent implements OnInit {

  recipientGroupForm = new FormGroup({
    groupName: new FormControl(''),
    description: new FormControl(''),
    emailAddress: new FormControl(''),
    emailAddressList: new FormGroup({
      recipientId : new FormControl(''),
      emailAddress : new FormControl(''),
    })
  });

  recipientGroupList: RecipientGroup[] = [];
  recipientGroupData: RecipientGroup[] = [];
  recipientList: Recipient[] = [];

  isShown: boolean = false;  
  isAddNew: boolean = true;
  recipientGroupId: number = 0;
  controllerName = "recipient-group";

  recipient = {
    recipientId: 0,
    emailAddress : ''
  }

  constructor(private http : HttpService, private fB : FormBuilder, private notifyService : NotificationService ) {
  }  

  
  getRecipientGroup(){
    this.isShown = true;
    this.http.getAll(this.controllerName).subscribe(res => {
      this.recipientGroupList = res;
    });
  }

  addRecipientGroup(){
    this.isShown = false;   
    this.isAddNew = true; 
    this.recipientList = []; 
    const data = {
      groupName : '',
      description : '',
      emailAddress: ''
    };
    this.createRecipientGroupForm(data);
  }
  
  saveRecipientGroup(){
    this.recipientGroupData = this.recipientGroupForm.value;

    const data = Object.assign({}, this.recipientGroupData);

    this.http.create(this.controllerName,data)
      .subscribe({
        next: (res) => {
          this.getRecipientGroup();
          // this.notifyService.showSuccess("Adding template", "Success")
        },
        error: (e) => console.error(e)
      });   
  }

  createRecipientGroupForm(data : any){
    this.recipientGroupForm = this.fB.group({
      groupName : [data.groupName],
      description : [data.description],
      emailAddress : [data.emailAddress]
    });
  }
  
  editRecipientGroup(id : any){ 
    this.recipientList = []; 
    this.recipientGroupId = id;
    this.http.get(this.controllerName,id).subscribe(res => {      
      this.isShown = false;
      this.isAddNew = false;  
      this.createRecipientGroupForm(res);
    });  
  }

  updateRecipientGroup(){
    this.recipientGroupData = this.recipientGroupForm.value;

    const data = Object.assign({}, this.recipientGroupData);

    this.http.update(this.controllerName, this.recipientGroupId, data)
      .subscribe({
        next: (res) => {
          this.getRecipientGroup();
        },
        error: (e) => console.error(e)
      });  
  }

  deleteRecipientGroup(id : any){
    this.http.delete(this.controllerName, id).subscribe(res => {
      this.getRecipientGroup();
    });   
  }

  editRecipient(recipient : any){  
   
  }

  deleteRecipient(recipient : any){
     
  }

  addRecipient(){
    const data : any = this.recipientGroupForm.get('emailAddress') ;
    this.recipient = {
      recipientId : 0,
      emailAddress: data.value
    }
    this.recipientList.push(this.recipient);
  }

  ngOnInit(): void {
    this.isShown = ! this.isShown;
    this.getRecipientGroup();
  }

}
