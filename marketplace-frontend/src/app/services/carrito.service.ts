import { Injectable } from '@angular/core';
import { ICarritoProductos, IProducto2 } from './models/IProductos';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CarritoService {
  private _productosCarrito:ICarritoProductos[] = []
  contador = 0
  totalCarrito = 0;
  contadorObservable = new BehaviorSubject<number>(0)
  carritoObservable = new BehaviorSubject<ICarritoProductos[]>([])
  totalCarritoObservable = new BehaviorSubject<number>(0)

  añadirCarrito(producto:IProducto2){
    let idProducto = producto.productoId;
    let indiceProducto = this._productosCarrito.findIndex(p=>p.producto.productoId === idProducto);
    if(indiceProducto === -1){
      this._productosCarrito.push({producto:producto,cantidad:1,total:producto.precio})
      this.totalCarrito = this.totalCarrito + producto.precio
      this.actualizarContador()
      console.log(this._productosCarrito);
      
    }
    else{
      let productoDetalle = this._productosCarrito[indiceProducto];
      productoDetalle.cantidad = productoDetalle.cantidad + 1;
      productoDetalle.total = productoDetalle.cantidad * productoDetalle.producto.precio
      this.totalCarrito = this.totalCarrito + productoDetalle.producto.precio
      this.actualizarContador()
      console.log(this._productosCarrito);
      
    }

    
    
  }
  
  eliminarDelCarrito(producto:IProducto2){
    let idProducto = producto.productoId
    let indiceProducto = this._productosCarrito.findIndex(p=>p.producto.productoId === idProducto);
    this.contador = this.contador - this._productosCarrito[indiceProducto].cantidad 
    this.totalCarrito = this.totalCarrito - this._productosCarrito[indiceProducto].total
    this._productosCarrito = this._productosCarrito.filter(p => p.producto.productoId !== idProducto);
    this.contadorObservable.next(this.contador);
    this.carritoObservable.next(this._productosCarrito);
    this.totalCarritoObservable.next(this.totalCarrito);
  }
  actualizarContador(){
    this.contador = this.contador + 1;
    this.contadorObservable.next(this.contador);
    this.carritoObservable.next(this._productosCarrito);
    this.totalCarritoObservable.next(this.totalCarrito);
  }
  limpiarCarrito(){
    this.contador = 0;
    this.totalCarrito = 0;
    this.contadorObservable.next(this.contador);
    this.carritoObservable.next([]);
    this.totalCarritoObservable.next(this.totalCarrito);
  }
}
