import { Component, inject } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ProductosApiService } from '../services/productos-api.service';
import { IProductoAñadir } from '../services/models/IProductos';

@Component({
  selector: 'app-formulario-producto',
  imports: [NavbarComponent,ReactiveFormsModule,RouterLink],
  templateUrl: './formulario-producto.component.html',
  styleUrl: './formulario-producto.component.scss'
})
export class FormularioProductoComponent {
  private readonly _formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private readonly _productsApi = inject(ProductosApiService);
    
  formGroup = this._formBuilder.nonNullable.group({
    nombre : ['',Validators.required],
    descripcion : ['',Validators.required],
    precio : ['',Validators.required],
    stock : ['',Validators.required],
  })
  

  registrarProducto() {
    let nuevoProducto: IProductoAñadir = {
    nombre: this.formGroup.value.nombre!,
    descripcion: this.formGroup.value.descripcion!,
    precio: Number(this.formGroup.value.precio),
    stock: Number(this.formGroup.value.stock),
    categoriaId: 1
  };
  this._productsApi.postProducto(nuevoProducto).subscribe({
    next: (producto) => {
      console.log('Producto creado:', producto);
      this.router.navigateByUrl("/perfil")
    },
    error: (err) => {
      console.error('Error al crear producto:', err);
    }
  });
  // console.log(nuevoProducto);
  
}


}
