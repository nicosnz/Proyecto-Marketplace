import { Component, inject, OnInit } from '@angular/core';
import { FooterComponent } from '../../footerPage/footer/footer.component';
import {  NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductosApiService } from '../../../services/productos-api.service';
import { ProductoComponent } from '../../tiendaPage/producto/producto.component';
import { IProducto } from '../../../services/models/productos/IProducto';
import { ICategoria } from '../../../services/models/ICategoria';
import { Router } from '@angular/router';

@Component({
  selector: 'app-menu',
  imports: [NavbarComponent,FooterComponent,ProductoComponent],
  templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit{
  private readonly _productsApi = inject(ProductosApiService);
  private router = inject(Router);

  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  productosGenerales:IProducto[] = []
  categorias:ICategoria[] = []
  agregarProducto(){
    if(this.isLoggedIn){
      this.router.navigateByUrl("/añadir-formulario");

    }
    else{
      this.router.navigateByUrl("/iniciar-sesion")
    }
  }
  
  ngOnInit(): void {
    this._productsApi.getProducts().subscribe((data)=>this.productosGenerales = data);
    this._productsApi.getCategorias().subscribe((data)=>this.categorias = data);
  }

  seleccionarCategoria(categoria:ICategoria){
    this.router.navigate(['/tienda'], { queryParams: { categoriaId: categoria.categoriaId } });
  }
  
}
