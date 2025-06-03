import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { IProducto } from './models/IProductos';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  hhtpCliente = inject(HttpClient);
  private readonly URL = "http://localhost:5038/api/Productos";
  getProducts(){
    return this.hhtpCliente.get<IProducto[]>(this.URL);
  }

}
