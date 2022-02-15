import { Component, OnInit } from '@angular/core';
import { HttpService } from 'src/app/core/services/https/http.service';
import { NotificationService } from 'src/app/core/services/notifications/notification.service';
import { FormControl, FormGroup, FormBuilder } from '@angular/forms';
import { faEdit, faTrash, faEye, faSave, faRemove } from '@fortawesome/free-solid-svg-icons';
import { ModelService } from 'src/app/core/services/modal/model.service';

interface RecipientGroup {
  recipientGroupId: any,
  groupName: string;
  createdDate: string;
}

interface Recipient {
  recipientId: any,
  emailAddress: string,
  isEdit: boolean
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
    emailAddress: new FormControl('')
  });

  recipientGroupList: RecipientGroup[] = [];
  recipientGroupData: RecipientGroup[] = [];
  recipientList: Recipient[] = [];

  faEdit = faEdit;
  faDelete = faTrash;
  faView = faEye;
  faSave = faSave;
  faRemove = faRemove;

  isShown: boolean = false;
  isAddNew: boolean = true;
  recipientGroupId: number = 0;
  controllerName = "recipient-group";
  IsRecordFetching: boolean = false;

  recipient = {
    recipientId: 0,
    emailAddress: '',
    isEdit: false
  }

  constructor(private http: HttpService, private fB: FormBuilder, private notifyService: NotificationService,
    private modelService: ModelService) {
  }


  getRecipientGroup() {
    this.isShown = true;
    this.IsRecordFetching = true;
    this.http.getAll(this.controllerName).subscribe(res => {
      this.IsRecordFetching = false;
      this.recipientGroupList = res;
    });
  }

  addRecipientGroup() {
    this.isShown = false;
    this.isAddNew = true;
    this.recipientList = [];
    const data = {
      groupName: '',
      description: '',
      emailAddress: ''
    };
    this.createRecipientGroupForm(data);
  }

  saveRecipientGroup() {
    const data = this.recipientGroupForm.value;
    data.recipientList = this.recipientList;

    this.http.create(this.controllerName, data)
      .subscribe({
        next: (res) => {
          this.notifyService.showSuccess("Recipient group saved successfully.", "Success");
          this.getRecipientGroup();
        },
        error: (e) => console.error(e)
      });
  }

  createRecipientGroupForm(data: any) {
    this.recipientGroupForm = this.fB.group({
      groupName: [data.groupName],
      description: [data.description],
      emailAddress: [data.emailAddress]
    });
  }

  editRecipientGroup(id: any) {
    this.recipientList = [];
    this.recipientGroupId = id;
    this.http.get(this.controllerName, id).subscribe(res => {
      this.isShown = false;
      this.isAddNew = false;
      this.createRecipientGroupForm(res);
      this.recipientList = JSON.parse(res.recipientListData);
    });
  }

  updateRecipientGroup() {
    const data = this.recipientGroupForm.value;
    data.recipientList = this.recipientList;

    this.http.update(this.controllerName, this.recipientGroupId, data)
      .subscribe({
        next: (res) => {
          this.notifyService.showSuccess("Recipient group updated successfully.", "Success");
          this.getRecipientGroup();
        },
        error: (e) => console.error(e)
      });
  }

  deleteRecipientGroup(id: any) {
    this.modelService.confirm('Confirmation', 'Are you sure you want to delete this recipient group?', 'Yes', 'No', 'md')
      .then((confirmed) => {
        if (confirmed) {
          this.http.delete(this.controllerName, id).subscribe(res => {
            this.notifyService.showSuccess("Recipient group deleted successfully.", "Success");
            this.getRecipientGroup();
          });
        }
      });
  }

  editRecipient(recipient: any) {
    recipient.isEdit = true;
  }

  deleteRecipient(index: number) {
    this.recipientList.splice(index, 1);
  }

  updateRecipient(recipient: any) {

  }

  cancelRecipient(recipient: any) {
    recipient.isEdit = false;

  }

  addRecipient() {
    const data: any = this.recipientGroupForm.get('emailAddress');
    this.recipient = {
      recipientId: 0,
      emailAddress: data.value,
      isEdit: false
    }
    this.recipientList.push(this.recipient);
  }

  ngOnInit(): void {
    this.isShown = !this.isShown;
    this.IsRecordFetching = !this.IsRecordFetching;
    this.getRecipientGroup();
  }

}
