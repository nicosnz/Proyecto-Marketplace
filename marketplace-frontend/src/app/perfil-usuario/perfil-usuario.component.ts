import { Component, inject } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductosApiService } from '../services/productos-api.service';
import { IProducto } from '../services/models/IProductos';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-perfil-usuario',
  imports: [NavbarComponent,RouterLink],
  templateUrl: './perfil-usuario.component.html',
  styleUrl: './perfil-usuario.component.scss'
})
export class PerfilUsuarioComponent {
  private router = inject(Router);

  private readonly _productsApi = inject(ProductosApiService);
  productos:IProducto[] = []
  ngOnInit():void{
    this._productsApi.getMyProducts().subscribe((data)=>this.productos = data);
  }
  logout(){
    localStorage.removeItem('token');
    localStorage.removeItem('usuario');
    this.router.navigateByUrl('/home')    
  }
}
