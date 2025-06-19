import { Component, Input, inject } from '@angular/core';
import { IProducto, IProducto2 } from '../../../services/models/IProductos';
import { CarritoService } from '../../../services/carrito.service';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-producto',
  imports: [RouterLink],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.scss'
})
export class ProductoComponent {
  @Input({required:true}) producto?:IProducto2;
  private _carritoService = inject(CarritoService);
  agregarCarrito(){
    this._carritoService.añadirCarrito(this.producto!);
  }
  
}
