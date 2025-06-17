import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { IAutentificacionResponse } from './models/autentificacion-response';
import { IUsuarioLogin, IUsuarioRegistrarse } from './models/IUsuarios';

@Injectable({
  providedIn: 'root'
})
export class AutentificacionService {
  hhtpCliente = inject(HttpClient);
  private readonly URL = "http://localhost:5038/api/Usuario/login";
  private readonly URL2 = "http://localhost:5038/api/Usuario/registrar";
  
  login(data:IUsuarioLogin):Observable<IAutentificacionResponse>{
    return this.hhtpCliente.post<IAutentificacionResponse>(this.URL,data).pipe(tap((response)=>{
        if(response){
          localStorage.setItem('token', response.token);
        }
    }))
  }
  registrarse(data:IUsuarioRegistrarse):Observable<IAutentificacionResponse>{
    return this.hhtpCliente.post<IAutentificacionResponse>(this.URL2,data).pipe(tap((response)=>{
        if(response){
          localStorage.setItem('token', response.token);
          if (response.usuario) {
          localStorage.setItem('usuario', JSON.stringify(response.usuario));
        }
        }
    }))
  
    
    
  }
}
