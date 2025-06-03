import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductoComponent } from "../producto/producto.component";
import { ProductosApiService } from '../services/productos-api.service';
import { IProducto } from '../services/models/IProductos';

@Component({
  selector: 'app-tienda',
  imports: [NavbarComponent, ProductoComponent],
  templateUrl: './tienda.component.html',
  styleUrl: './tienda.component.scss'
})
export class TiendaComponent implements OnInit {
  private readonly _productsApi = inject(ProductosApiService);
  productos:IProducto[] = []
  ngOnInit():void{
    this._productsApi.getProducts().subscribe((data)=>this.productos = data);
  }
  
  
  
}
