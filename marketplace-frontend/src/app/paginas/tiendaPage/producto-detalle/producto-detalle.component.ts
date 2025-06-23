import { Component, inject, Input, OnInit } from '@angular/core';
import { IProducto, IProducto2 } from '../../../services/models/IProductos';
import { ProductosApiService } from '../../../services/productos-api.service';
import { routes } from '../../../app.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { DetalleOrdenComponent } from "../../pagos/detalle-orden/detalle-orden.component";
import { FooterComponent } from "../../footerPage/footer/footer.component";
import { NgClass } from '@angular/common';
import { CarritoService } from '../../../services/carrito.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IComentarios } from '../../../services/models/IComentarios';

@Component({
  selector: 'app-producto-detalle',
  imports: [NavbarComponent,FooterComponent,NgClass,ReactiveFormsModule],
  templateUrl: './producto-detalle.component.html',
  styleUrl: './producto-detalle.component.scss'
})
export class ProductoDetalleComponent implements OnInit {
  producto?:IProducto2;
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
