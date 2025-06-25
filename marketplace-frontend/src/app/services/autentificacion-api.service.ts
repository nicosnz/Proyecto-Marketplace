import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IAutentificacionResponse } from './models/usuarios/autentificacion-response';
import { environment } from '../../environments/environment';
import { IUsuarioLogin } from './models/usuarios/IUsuarioLogin';
import { IUsuarioRegistrarse } from './models/usuarios/IUsuarioRegistrarse';

@Injectable({
  providedIn: 'root'
})
export class AutentificacionApiService {
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
