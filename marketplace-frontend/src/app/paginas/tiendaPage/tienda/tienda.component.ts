import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductoComponent } from "../producto/producto.component";
import { ProductosApiService } from '../../../services/productos-api.service';
import { FooterComponent } from '../../footerPage/footer/footer.component';
import { ICategoria } from '../../../services/models/ICategoria';
import { FormsModule } from '@angular/forms';
import { IProducto } from '../../../services/models/productos/IProducto';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-tienda',
  imports: [NavbarComponent, ProductoComponent,FooterComponent,FormsModule,NgClass],
  templateUrl: './tienda.component.html',
  styleUrl: './tienda.component.scss'
})
export class TiendaComponent implements OnInit {
  private readonly _productsApi = inject(ProductosApiService);
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  productosGenerales:IProducto[] = []
  productosCatalogo:IProducto[] = []
  categorias:ICategoria[] = []
  productosPorCategoria:IProducto[] = []
  categoriaSeleccionada: ICategoria | null = null;
  ngOnInit():void{
    this._productsApi.getCategorias().subscribe((categorias) => this.categorias = categorias)
     
    this._productsApi.getProducts().subscribe((data)=>this.productosGenerales=data);
    this._productsApi.getProductsCatalogo().subscribe((data)=> this.productosCatalogo = data);

    
  }

  buscarPorCategoria(categoria: ICategoria) {
    if (this.categoriaSeleccionada && this.categoriaSeleccionada.categoriaId === categoria.categoriaId) {
      this.categoriaSeleccionada = null;
      this._productsApi.getProductsCatalogo().subscribe((data) => {
        this.productosCatalogo = data;
      });
    } else {
      this.categoriaSeleccionada = categoria;
      this._productsApi.getProductosPorCategoria(categoria.categoriaId).subscribe((data) => {
        this.productosPorCategoria = data;
        this.productosCatalogo = this.productosPorCategoria;
      });
    }
  }
  searchTerm: string = '';
  get productosFiltrados(): IProducto[] {
    if (!this.searchTerm) return this.productosCatalogo;
    const term = this.searchTerm.toLowerCase();
    return this.productosCatalogo.filter(p =>
      p.nombre.toLowerCase().startsWith(term) ||
      (p.descripcion && p.descripcion.toLowerCase().startsWith(term))
    );
  }
  get productosFiltradosSinToken(): IProducto[] {
    if (!this.searchTerm) return this.productosGenerales;
    const term = this.searchTerm.toLowerCase();
    return this.productosGenerales.filter(p =>
      p.nombre.toLowerCase().startsWith(term) ||
      (p.descripcion && p.descripcion.toLowerCase().startsWith(term))
    );
  }
  
  
  
}
