import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { ProductosApiService } from '../../../services/productos-api.service';
import { FooterComponent } from "../../footerPage/footer/footer.component";
import { ICategoria } from '../../../services/models/ICategoria';
import { IProductoEditar } from '../../../services/models/productos/IProductoEditar';

@Component({
  selector: 'app-formulario-producto',
  imports: [NavbarComponent, ReactiveFormsModule, RouterLink, FooterComponent],
  templateUrl: './formulario-producto.component.html',
})
export class FormularioProductoComponent implements OnInit {
  private readonly _formBuilder = inject(FormBuilder);
  private router = inject(Router);
  private readonly _productsApi = inject(ProductosApiService);
  private route = inject(ActivatedRoute);
  modoEditar = false;
  categorias:ICategoria[] = []
  mensajeError:string|null = null
  mensajeExito:string|null = null

  formGroup = this._formBuilder.nonNullable.group({
    nombre : ['',Validators.required],
    descripcion : ['',Validators.required],
    precio : [0,Validators.required],
    stock : [0,Validators.required],
    categoriaId : ['',Validators.required]
  })
  imagenFile: File | null = null;
  productoId: number | null = null;
  ngOnInit() {
    this._productsApi.getCategorias().subscribe((data)=>this.categorias=data)
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.modoEditar = true;
        this.productoId = Number(id);
        this._productsApi.getProduct(this.productoId).subscribe({
          next: (product) => {
            this.formGroup.patchValue({
              nombre: product.nombre,
              descripcion: product.descripcion,
              precio:product.precio,
              stock: product.stock,
              categoriaId:product.categoriaId.toString()
            });
          }
        });
      }
    });
  }
  

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.imagenFile = input.files[0];
    }
  }
  

  registrarProducto() {
  
   const formData = new FormData();
    formData.append('nombre', this.formGroup.value.nombre!);
    formData.append('descripcion', this.formGroup.value.descripcion!);
    formData.append('precio', this.formGroup.value.precio!.toString());
    formData.append('stock', this.formGroup.value.stock!.toString());
    formData.append('categoriaId', this.formGroup.value.categoriaId!); 
    

    if (this.imagenFile) {
      formData.append('imagen', this.imagenFile);
    }
    this._productsApi.postProducto(formData).subscribe({
      next: (producto) => {
        this.mensajeError = null;
        this.mensajeExito = "Exito!";
        setTimeout(() => {
            this.mensajeExito = null;
            this.router.navigateByUrl("/perfil")

          }, 1000);
      },
      error: (err) => {
        this.mensajeExito = null;
        this.mensajeError =  err.error.errors;
      }
    });
  
  }

  actualizarProducto(){
    if (this.formGroup.valid) {
      const productoEditado: IProductoEditar = {
        productoId: this.productoId!, 
        nombre: this.formGroup.value.nombre!,
        descripcion: this.formGroup.value.descripcion,
        precio: Number(this.formGroup.value.precio),
        stock: Number(this.formGroup.value.stock),
        categoriaId:Number(this.formGroup.value.categoriaId)
      };
      

      this._productsApi.putProducto(productoEditado).subscribe({
        next: (respuesta) => {
          this.mensajeError = null;
          this.mensajeExito = "Exito!";
          setTimeout(() => {
              this.mensajeExito = null;
              this.router.navigateByUrl("/perfil")

            }, 1000);
        },
        error: (err) => {
          console.error(err.error.mensaje);
        }
    });
    } else {
      this.formGroup.markAllAsTouched();
    }

  }


}
