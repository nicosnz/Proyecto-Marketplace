import { Component, inject } from '@angular/core';
import { NavbarComponent } from "../navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ProductosApiService } from '../services/productos-api.service';
import { IProductoAñadir } from '../services/models/IProductos';

@Component({
  selector: 'app-formulario-producto',
  imports: [NavbarComponent,ReactiveFormsModule],
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
  nuevoProducto: IProductoAñadir = {
  nombre: this.formGroup.value.nombre!,
  descripcion: this.formGroup.value.descripcion!,
  precio: this.formGroup.value.precio as number!,
  stock: 0,
  categoriaId: 1
};

registrarProducto() {
  this._productsApi.postProducto(this.nuevoProducto).subscribe({
    next: (producto) => {
      console.log('Producto creado:', producto);
      // Navegar, mostrar alerta o resetear formulario
    },
    error: (err) => {
      console.error('Error al crear producto:', err);
    }
  });
}


}
