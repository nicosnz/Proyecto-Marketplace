import { Component, inject, OnInit } from '@angular/core';
import { ICarritoProductos } from '../../../services/models/IProductos';
import { CarritoService } from '../../../services/carrito.service';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-detalle-orden',
  imports: [NavbarComponent,RouterLink],
  templateUrl: './detalle-orden.component.html',
  styleUrl: './detalle-orden.component.scss'
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
