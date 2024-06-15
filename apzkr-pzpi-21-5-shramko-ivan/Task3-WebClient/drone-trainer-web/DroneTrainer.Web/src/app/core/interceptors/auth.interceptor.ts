import { Injectable } from '@angular/core';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpHeaders,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private Router: Router) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    if (req.url.includes('/api/token')) {
      return next.handle(req);
    }

    const accessToken = localStorage.getItem('accessToken');
    if (accessToken) {
      const cloned = req.clone({
        headers: new HttpHeaders({
          Authorization: `Bearer ${accessToken}`,
        }),
      });
      return next.handle(cloned);
    }

    return next.handle(req);
  }
}
