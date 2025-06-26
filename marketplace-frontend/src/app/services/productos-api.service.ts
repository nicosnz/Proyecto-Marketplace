import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { ICategoria } from './models/ICategoria';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { IComentarios } from './models/IComentarios';
import { IProductoEditar } from './models/productos/IProductoEditar';
import { IProducto } from './models/productos/IProducto';

@Injectable({
  providedIn: 'root'
})
export class ProductosApiService {
  httpCliente = inject(HttpClient);
  private readonly URL_PRODUCTOS_USUARIO = `${environment.urlApi}/Productos/mis-productos`;
  private readonly URL_PRODUCTOS = `${environment.urlApi}/Productos`;
  private readonly URL_AÑADIR_PRODUCTOS = `${environment.urlApi}/Productos/añadir`;
  private readonly URL_EDITAR_PRODUCTO = `${environment.urlApi}/Productos/editar`;
  private readonly URL_ELIMINAR_PRODUCTO = `${environment.urlApi}/Productos/eliminar`;
  private readonly URL_PRODUCTO = `${environment.urlApi}/Productos/producto`;
  private readonly URL_PRODUCTOS_CATALOGO = `${environment.urlApi}/Productos/catalogo`;
  private readonly URL_CATEGORIAS = `${environment.urlApi}/Productos/categorias`;
  private readonly URL_PRODUCTOS_POR_CATEGORIA = `${environment.urlApi}/Productos/categoria`;
  private readonly URL_AÑADIR_COMENTARIO = `${environment.urlApi}/Productos/producto/añadirComentario`;
  private readonly URL_AÑADIR_FAVORITO = `${environment.urlApi}/Productos/añadirAfavoritos`;
  private readonly URL_ELIMINAR_FAVORITO = `${environment.urlApi}/Productos/eliminarDeFavoritos`;

  getMyProducts():Observable<IProducto[]>{
    return this.httpCliente.get<IProducto[]>(this.URL_PRODUCTOS_USUARIO);
  }
  getProducts():Observable<IProducto[]>{
    return this.httpCliente.get<IProducto[]>(this.URL_PRODUCTOS); 
  }
  postProducto(formData:FormData):Observable<IProducto>{
    return this.httpCliente.post<IProducto>(this.URL_AÑADIR_PRODUCTOS,formData)
  }
  getProduct(idProducto:number){
    return this.httpCliente.get<IProducto>(`${this.URL_PRODUCTO}/${idProducto}`);

  }
  getProductosPorCategoria(idCategoria:number){
    return this.httpCliente.get<IProducto[]>(`${this.URL_PRODUCTOS_POR_CATEGORIA}/${idCategoria}`);
  }
  getCategorias(){
    return this.httpCliente.get<ICategoria[]>(this.URL_CATEGORIAS)
  }
  getProductsCatalogo(){
    return this.httpCliente.get<IProducto[]>(this.URL_PRODUCTOS_CATALOGO); 
  }
  putProducto(producto:IProductoEditar){
    return this.httpCliente.put<IProductoEditar>(`${this.URL_EDITAR_PRODUCTO}/${producto.productoId}`,producto)
  
  }
  deleteProducto(idProducto:number){
    return this.httpCliente.delete(`${this.URL_ELIMINAR_PRODUCTO}/${idProducto}`);
  }
  postFavoritos(idProducto:number){
    return this.httpCliente.post(`${this.URL_AÑADIR_FAVORITO}/${idProducto}`,{})
  
  }
  deleteFavoritos(idProducto:number){
    return this.httpCliente.delete(`${this.URL_ELIMINAR_FAVORITO}/${idProducto}`);
  }

  postComentario(idProducto: number, comentario: IComentarios) {
    
    return this.httpCliente.post(
      `${this.URL_AÑADIR_COMENTARIO}/${idProducto}`,
      comentario
    );
    
  }
}
