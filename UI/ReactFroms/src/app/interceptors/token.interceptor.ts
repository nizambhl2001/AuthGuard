import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class MyInterceptor implements HttpInterceptor {
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // You can modify the request here, for example, add headers
    const modifiedRequest = request.clone({
      setHeaders: {
        Authorization: 'Bearer myAccessToken'
      }
    });

    // Pass the modified request to the next interceptor or handler
    return next.handle(modifiedRequest);
  }
}
