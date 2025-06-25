import { Component, inject, OnInit } from '@angular/core';
import { ProductosApiService } from '../../../services/productos-api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FooterComponent } from "../../footerPage/footer/footer.component";
import { NgClass } from '@angular/common';
import { CarritoService } from '../../../services/carrito.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IComentarios } from '../../../services/models/IComentarios';
import { IProducto } from '../../../services/models/productos/IProducto';

@Component({
  selector: 'app-producto-detalle',
  imports: [NavbarComponent,FooterComponent,NgClass,ReactiveFormsModule],
  templateUrl: './producto-detalle.component.html'
})
export class ProductoDetalleComponent implements OnInit {
  producto?:IProducto;
  private _carritoService = inject(CarritoService);
  private readonly _formBuilder = inject(FormBuilder);
  private _productsService = inject(ProductosApiService)
  private _route = inject(ActivatedRoute)
  private readonly _router = inject(Router)

  
  formGroup = this._formBuilder.nonNullable.group({
    comentario : ['',Validators.required],
    
  })
  get isLoggedIn(): boolean {
      return !!localStorage.getItem('token');
  }
  
  ngOnInit(){
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this._productsService.getProduct(+id).subscribe((prod) => this.producto = prod);
    }

  }
  agregarCarrito(){
    if(this.isLoggedIn){

      this._carritoService.añadirCarrito(this.producto!);
    }
    else{
      this._router.navigateByUrl("/iniciar-sesion")
    }
  }
  agregarComentario(){
    const id = this._route.snapshot.paramMap.get('id');

    let comentarioProducto:IComentarios = {  
      usuarioId : 1,
      nombreUsuario :"xd",
      texto : this.formGroup.value.comentario!
    };
    if(id){
      console.log(this.formGroup.value.comentario,this.producto?.productoId);

      this._productsService.postComentario(+id,comentarioProducto).subscribe({
        next: (res) => console.log(res),
        error: (err) => { console.error('error al comentar', err); }
      });;
    }

  }
}
