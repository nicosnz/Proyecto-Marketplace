import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UsuariosApiService {
  hhtpCliente = inject(HttpClient)
  constructor() { 
    console.log("hola");
    
  }
}
