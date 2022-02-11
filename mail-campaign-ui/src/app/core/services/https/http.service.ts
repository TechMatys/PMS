import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';
// import { Environments } from 'src/environments/environment';
 
@Injectable({
  providedIn: 'root'
})
export class HttpService {
  apiUrl = "https://localhost:44395/";
  constructor(private http : HttpClient) { }

  getAll(contrl : any): Observable < any[] > {
    return this.http.get< any >(this.apiUrl + contrl);
  }

  get(contrl : any, id: any): Observable<any> {
    return this.http.get(`${this.apiUrl + contrl}/${id}`);
  }

  create(contrl: any,data: any): Observable<any> {
    return this.http.post(this.apiUrl + contrl, data);
  }

  update(contrl : any, id: any, data: any): Observable<any> {
    return this.http.patch(`${this.apiUrl + contrl}/${id}`, data);
  }

  post(contrl : any, val: any) {  
    return this.http.post(this.apiUrl + contrl, val);  
  }  

  delete(contrl : any,val: any) {  
    return this.http.delete(this.apiUrl + contrl + '/' +val);  
  }   

}
