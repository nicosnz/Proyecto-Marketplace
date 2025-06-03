import { Component, Input } from '@angular/core';
import { IProducto } from '../services/models/IProductos';

@Component({
  selector: 'app-producto',
  imports: [],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.scss'
})
export class ProductoComponent {
  @Input({required:true}) producto?:IProducto;
  print(){
    console.log(this.producto)
  }

}
