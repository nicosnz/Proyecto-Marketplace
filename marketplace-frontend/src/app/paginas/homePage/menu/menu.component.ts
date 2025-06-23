import { Component, inject, OnInit } from '@angular/core';
import { FooterComponent } from '../../footerPage/footer/footer.component';
import {  NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductosApiService } from '../../../services/productos-api.service';
import { IProducto2 } from '../../../services/models/IProductos';
import { ProductoComponent } from '../../tiendaPage/producto/producto.component';

@Component({
  selector: 'app-menu',
  imports: [NavbarComponent,FooterComponent,ProductoComponent],
  templateUrl: './menu.component.html'
})
export class MenuComponent implements OnInit{
  private readonly _productsApi = inject(ProductosApiService);
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  productosGenerales:IProducto2[] = []
  
  ngOnInit(): void {
    this._productsApi.getProducts().subscribe((data)=>this.productosGenerales = data)

  }

  
}
