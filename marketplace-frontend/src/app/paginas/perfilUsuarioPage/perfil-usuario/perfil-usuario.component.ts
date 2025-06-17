import { IProducto, IProducto2 } from './../../../services/models/IProductos';
import { Component, inject } from '@angular/core';

import { Router, RouterLink } from '@angular/router';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductosApiService } from '../../../services/productos-api.service';
import { FooterComponent } from "../../footerPage/footer/footer.component";

@Component({
  selector: 'app-perfil-usuario',
  imports: [NavbarComponent, RouterLink, FooterComponent],
  templateUrl: './perfil-usuario.component.html',
  styleUrl: './perfil-usuario.component.scss'
})
export class PerfilUsuarioComponent {
  private router = inject(Router);
  mostrar_modal = false;
  productoAeliminar:number|any;

  private readonly _productsApi = inject(ProductosApiService);
  productos:IProducto2[] = []
  ngOnInit():void{
    this._productsApi.getMyProducts().subscribe((data)=>this.productos = data);
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.router.navigateByUrl('/home')    
  }
  mostrarModal(idProducto:number){
     this.mostrar_modal = true;
     this.productoAeliminar = idProducto;
  }
  cerrarModal(){
     this.mostrar_modal = false;
  }
  eliminarProducto(idProducto:number){
    this._productsApi.deleteProducto(idProducto).subscribe({
      next:()=>{
        this.productos = this.productos.filter(p => p.productoId !== idProducto);
        this.cerrarModal();
      },
      error:(err)=>{
        console.log(err);
        
      }
    })
  }
  editarProducto(producto:IProducto2){
    this.router.navigate(['/añadir-formulario', producto.productoId]);
  }
}
