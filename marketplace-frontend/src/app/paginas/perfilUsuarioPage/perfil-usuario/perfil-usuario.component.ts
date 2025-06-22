import { OrdenesService } from './../../../services/ordenes.service';
import { IProducto, IProducto2 } from './../../../services/models/IProductos';
import { Component, inject } from '@angular/core';

import { Router, RouterLink } from '@angular/router';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductosApiService } from '../../../services/productos-api.service';
import { FooterComponent } from "../../footerPage/footer/footer.component";
import { IMisCompras, IMisVentas, OrdenConProductos } from '../../../services/models/IOrdenes';
import { UsuariosApiService } from '../../../services/usuarios-api.service';
import { IUsuarioInfo } from '../../../services/models/IUsuarios';

@Component({
  selector: 'app-perfil-usuario',
  imports: [NavbarComponent, RouterLink, FooterComponent],
  templateUrl: './perfil-usuario.component.html',
  styleUrl: './perfil-usuario.component.scss'
})
export class PerfilUsuarioComponent {
  private router = inject(Router);
  private readonly _OrdenesService = inject(OrdenesService);
  private readonly _usuarioService = inject(UsuariosApiService);
  mostrar_modal = false;
  productoAeliminar:number|any;
  seccionSeleccionada: 'info' | 'mis-productos' | 'vendidos' | 'comprados' = 'info';
  misCompras:IMisCompras[] = []
  misVentas:IMisVentas[]=[]
  ordenes: OrdenConProductos[] = [];
  infoUsuario:IUsuarioInfo|any;

  private readonly _productsApi = inject(ProductosApiService);
  productos:IProducto2[] = []
  ngOnInit():void{
    this._productsApi.getMyProducts().subscribe((data)=>this.productos = data);
    this._OrdenesService.getMisCompras().subscribe((data)=>{
      this.misCompras = data;
      this.ordenes = this.agruparComprasPorOrden(this.misCompras);

    });
    this._OrdenesService.getMisVentas().subscribe((data)=>this.misVentas=data);
    this._usuarioService.getUsuario().subscribe((data)=>this.infoUsuario = data);
    
    
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
  agruparComprasPorOrden(compras: IMisCompras[]): OrdenConProductos[] {
    const ordenesMap = new Map<number, OrdenConProductos>();
    for (const compra of compras) {
      if (!ordenesMap.has(compra.ordenId)) {
        ordenesMap.set(compra.ordenId, {
          ordenId: compra.ordenId,
          fechaOrden: compra.fechaOrden,
          productos: [],
          total: 0,
        });
      }
      const orden = ordenesMap.get(compra.ordenId)!;
      orden.productos.push(compra);
      orden.total += compra.cantidad * compra.precioUnitario;
    }
    return Array.from(ordenesMap.values());
}

}
