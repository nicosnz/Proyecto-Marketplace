import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductoComponent } from "../producto/producto.component";
import { ProductosApiService } from '../../../services/productos-api.service';
import { IProducto, IProducto2 } from '../../../services/models/IProductos';
import { FooterComponent } from '../../footerPage/footer/footer.component';
import { ICategoria } from '../../../services/models/ICategoria';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-tienda',
  imports: [NavbarComponent, ProductoComponent,FooterComponent,FormsModule],
  templateUrl: './tienda.component.html',
  styleUrl: './tienda.component.scss'
})
export class TiendaComponent implements OnInit {
  private readonly _productsApi = inject(ProductosApiService);
  get isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }
  productos:IProducto2[] = []
  productos2:IProducto2[] = []
  categorias:ICategoria[] = []
  productosPorCategoria:IProducto2[] = []
  ngOnInit():void{
    this._productsApi.getProducts().subscribe((data)=>this.productos=data);
    this._productsApi.getProductsCatalogo().subscribe((data)=> this.productos2 = data);
    this._productsApi.getCategorias().subscribe((data)=>this.categorias = data);
  }
  buscarPorCategoria(categoria:ICategoria){
    this._productsApi.getProductosPorCategoria(categoria.categoriaId).subscribe((data)=>{
      this.productosPorCategoria = data
    console.log(this.productosPorCategoria)
    this.productos2 = this.productosPorCategoria;});
    
  }
  searchTerm: string = '';
  get productosFiltrados(): IProducto2[] {
    if (!this.searchTerm) return this.productos2;
    const term = this.searchTerm.toLowerCase();
    return this.productos2.filter(p =>
      p.nombre.toLowerCase().startsWith(term) ||
      (p.descripcion && p.descripcion.toLowerCase().startsWith(term))
    );
}
  
  
  
}
