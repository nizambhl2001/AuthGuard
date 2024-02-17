import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = "https://localhost:7044/api/User/"
  constructor(private http:HttpClient,private router:Router) { }


  logOut(){
    localStorage.clear();
    this.router.navigate(['login']);
  }
  login(userObj:any):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}UserLogin`,userObj)
  }
  register(userObj:any):Observable<any>{
    return this.http.post<any>(`${this.apiUrl}UserRegister`,userObj)
  }

  storeToken(tokenValue:string){
    localStorage.setItem('token',tokenValue)
  }
  getToken(){
    return localStorage.getItem('token');
  }
  isLoggedIn():boolean{
    return !!localStorage.getItem('token');
  }
}
