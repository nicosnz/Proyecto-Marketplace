import { Component, Input, inject } from '@angular/core';
import { CarritoService } from '../../../services/carrito.service';
import { Router, RouterLink } from '@angular/router';
import { IProducto } from '../../../services/models/productos/IProducto';

@Component({
  selector: 'app-producto',
  imports: [RouterLink],
  templateUrl: './producto.component.html'
})
export class ProductoComponent {
  @Input({required:true}) producto?:IProducto;
  private _carritoService = inject(CarritoService);
  private readonly _router = inject(Router)

  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  agregarCarrito(){
    if(this.isLoggedIn){
      this._carritoService.añadirCarrito(this.producto!);

    }
    else{
      this._router.navigateByUrl("/iniciar-sesion")
    }
  }
  
}
