import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthService } from '../service/auth.service';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';


@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private auth:AuthService,private router:Router){}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  
    var myToken = this.auth.getToken();
   if(myToken){
    request = request.clone({
      setHeaders:{Authorization:`Bearer ${myToken}`}
    })
   }
    return next.handle(request)
    // .pipe(
    //   catchError((err:any)=>{
    //     if(err instanceof HttpErrorResponse){
    //       if(err.status ==401){
    //         alert("Token is Exprire, Login again");
    //         this.router.navigate(['login'])
    //       }
    //     }
    //     return throwError(()=> new Error("Some Error"))
    //   })
    // ); 
    
  }
}
