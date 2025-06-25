import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { IUsuarioInfo } from './models/usuarios/IUsuarioInfo';

@Injectable({
  providedIn: 'root'
})
export class UsuariosApiService {
  hhtpCliente = inject(HttpClient)
  private readonly URL_OBTENER_INFO_USUARIO = `${environment.urlApi}/Usuario/infoUsuario`
  getUsuario(){
    return this.hhtpCliente.get<IUsuarioInfo>(this.URL_OBTENER_INFO_USUARIO)
  }
}
