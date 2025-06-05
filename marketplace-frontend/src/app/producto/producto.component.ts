import { Component, Input } from '@angular/core';
import { IProducto, IProducto2 } from '../services/models/IProductos';

@Component({
  selector: 'app-producto',
  imports: [],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.scss'
})
export class ProductoComponent {
  @Input({required:true}) producto?:IProducto2;
  
}
