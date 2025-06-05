import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const token = localStorage.getItem('token');
  const headers = req.headers.set('Authorization',`Bearer ${token}`!)
  const requestClon = req.clone({headers})
  return next(requestClon);
};
