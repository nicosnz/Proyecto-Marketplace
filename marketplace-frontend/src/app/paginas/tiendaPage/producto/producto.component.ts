import { Component, Input, inject } from '@angular/core';
import { CarritoService } from '../../../services/carrito.service';
import { Router, RouterLink } from '@angular/router';
import { IProducto } from '../../../services/models/productos/IProducto';
import { ProductosApiService } from '../../../services/productos-api.service';

@Component({
  selector: 'app-producto',
  imports: [RouterLink],
  templateUrl: './producto.component.html'
})
export class ProductoComponent {
  @Input({required:true}) producto?:IProducto;
  esFavorito:boolean|any = false;
  private _carritoService = inject(CarritoService);
  private _productoService = inject(ProductosApiService);
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
  Favorito() {
    if (!this.isLoggedIn) {
      this._router.navigateByUrl("/iniciar-sesion");
      return;
    }
    if (!this.esFavorito) {
      this._productoService.postFavoritos(this.producto!.productoId).subscribe({
        next: () => this.esFavorito = false,
        error: (error) =>console.log(error)
        
      });
    } else {
      this._productoService.deleteFavoritos(this.producto!.productoId).subscribe({
        next: () => this.esFavorito = true,
        error: (error) => console.log(error)
        
      });
    }
  }
  
}
