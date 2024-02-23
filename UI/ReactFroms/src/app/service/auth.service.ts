import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  apiUrl = "https://localhost:7044/api/User/";
  private UserPaylad:any
  constructor(private http:HttpClient,private router:Router) {this.UserPaylad = this.decodedToken() }


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

  decodedToken(){
    const jwtHelper = new JwtHelperService();
    const token = this.getToken();
    return jwtHelper.decodeToken(token);
  }

  getfullNameFromToken(){
    if(this.UserPaylad){
      return this.UserPaylad.name
    }
  }

  getroleFromToken(){
    if(this.UserPaylad){
      return this.UserPaylad.role
    }
  }
}
