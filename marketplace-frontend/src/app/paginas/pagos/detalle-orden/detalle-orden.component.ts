import { Component, inject, OnInit } from '@angular/core';
import { CarritoService } from '../../../services/carrito.service';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { RouterLink } from '@angular/router';
import { ICarritoProductos } from '../../../services/models/productos/ICarritoProductos';

@Component({
  selector: 'app-detalle-orden',
  imports: [NavbarComponent,RouterLink],
  templateUrl: './detalle-orden.component.html'
})
export class DetalleOrdenComponent implements OnInit {
  private _carritoService = inject(CarritoService);
  productosAcomprar:ICarritoProductos[] = []
  totalCarrito = 0;

  ngOnInit(): void {
    this._carritoService.carritoObservable.subscribe({
      next:(prod)=>{
        this.productosAcomprar = prod;
      }
    });
    this._carritoService.totalCarritoObservable.subscribe({
      next:(total)=>{
        this.totalCarrito = total;
      }
    });
  }

}
