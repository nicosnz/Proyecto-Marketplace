import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IAutentificacionResponse } from './models/autentificacion-response';
import { IUsuarioLogin, IUsuarioRegistrarse } from './models/IUsuarios';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AutentificacionService {
  hhtpCliente = inject(HttpClient);
  private readonly URL_LOGIN = `${environment.urlApi}/Usuario/login`;
  private readonly URL_REGISTRARSE = `${environment.urlApi}/Usuario/registrar`;
  
  login(data:IUsuarioLogin):Observable<IAutentificacionResponse>{
    return this.hhtpCliente.post<IAutentificacionResponse>(this.URL_LOGIN,data).pipe(tap((response)=>{
        if(response){
          localStorage.setItem('token', response.token);
        }
    }))
  }
  registrarse(data:IUsuarioRegistrarse):Observable<IAutentificacionResponse>{
    return this.hhtpCliente.post<IAutentificacionResponse>(this.URL_REGISTRARSE,data).pipe(tap((response)=>{
        if(response){
          localStorage.setItem('token', response.token);
          if (response.usuario) {
          localStorage.setItem('usuario', JSON.stringify(response.usuario));
        }
        }
    }))
  
    
    
  }
}
