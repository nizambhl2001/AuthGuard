import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  baseUrl = 'https://localhost:7044/api/User';
  constructor(private http:HttpClient) { }

  getUser():Observable<any>{
    return this.http.get<any>(this.baseUrl);
  }
}
