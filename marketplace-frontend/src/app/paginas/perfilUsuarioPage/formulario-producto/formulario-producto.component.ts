import { Component, inject } from '@angular/core';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ProductosApiService } from '../../../services/productos-api.service';
import { IProductoAñadir } from '../../../services/models/IProductos';
import { FooterComponent } from "../../footerPage/footer/footer.component";

@Component({
  selector: 'app-formulario-producto',
  imports: [NavbarComponent, ReactiveFormsModule, RouterLink, FooterComponent],
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
  imagenFile: File | null = null;

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.imagenFile = input.files[0];
    }
  }
  

  registrarProducto() {
  //   let nuevoProducto: IProductoAñadir = {
  //   nombre: this.formGroup.value.nombre!,
  //   descripcion: this.formGroup.value.descripcion!,
  //   precio: Number(this.formGroup.value.precio),
  //   stock: Number(this.formGroup.value.stock),
  //   categoriaId: 1
  // };
   const formData = new FormData();
    formData.append('nombre', this.formGroup.value.nombre!);
    formData.append('descripcion', this.formGroup.value.descripcion!);
    formData.append('precio', this.formGroup.value.precio!.toString());
    formData.append('stock', this.formGroup.value.stock!.toString());
    formData.append('categoriaId', '1'); 

    if (this.imagenFile) {
      formData.append('imagen', this.imagenFile);
    }
    this._productsApi.postProducto(formData).subscribe({
      next: (producto) => {
        console.log('Producto creado:', producto);
        
        this.router.navigateByUrl("/perfil")
      },
      error: (err) => {
        console.error('Error al crear producto:', err);
      }
    });
  
  }


}
