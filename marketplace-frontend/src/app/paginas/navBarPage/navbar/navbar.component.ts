import { Component, inject, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { CarritoService } from '../../../services/carrito.service';
import { ICarritoProductos, IProducto, IProducto2 } from '../../../services/models/IProductos';

@Component({
  selector: 'app-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.scss'
})
export class NavbarComponent implements OnInit {
  private _carritoService = inject(CarritoService);
  contador = 0;
  carritoAbierto = false;
  productosCarrito:ICarritoProductos[] = []
  totalCarrito = 0;
  get isLoggedIn(): boolean {
      return !!localStorage.getItem('token');
  }
  abrirCarrito(){
    this.carritoAbierto = true;
  }
  cerrarCarrito(){
    this.carritoAbierto = false;
  }
  ngOnInit(): void {
    this._carritoService.contadorObservable.subscribe({
      next:(cont)=> {
        this.contador = cont;
      }

      }
    );
    this._carritoService.carritoObservable.subscribe({
      next:(pro)=>{
        this.productosCarrito = pro;
        
      }
    });
    this._carritoService.totalCarritoObservable.subscribe({
      next:(total)=>{
        this.totalCarrito = total;
        
      }
    })
  }
  eliminarCarrito(producto:IProducto2){
    this._carritoService.eliminarDelCarrito(producto)
  }
  
  
}
