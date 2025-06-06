import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductoComponent } from "../producto/producto.component";
import { ProductosApiService } from '../../../services/productos-api.service';
import { IProducto, IProducto2 } from '../../../services/models/IProductos';
import { FooterComponent } from '../../footerPage/footer/footer.component';

@Component({
  selector: 'app-tienda',
  imports: [NavbarComponent, ProductoComponent,FooterComponent],
  templateUrl: './tienda.component.html',
  styleUrl: './tienda.component.scss'
})
export class TiendaComponent implements OnInit {
  private readonly _productsApi = inject(ProductosApiService);
  productos:IProducto2[] = []
  ngOnInit():void{
    this._productsApi.getProducts().subscribe((data)=>this.productos = data);
  }
  
  
  
}
