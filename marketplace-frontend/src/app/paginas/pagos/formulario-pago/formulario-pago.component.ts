import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CarritoService } from '../../../services/carrito.service';
import { ICarritoProductos, IProducto2 } from '../../../services/models/IProductos';
import { OrdenesService } from '../../../services/ordenes.service';
import { IUsuarioCompra } from '../../../services/models/IUsuarios';
import { Router } from '@angular/router';

@Component({
  selector: 'app-formulario-pago',
  imports: [NavbarComponent,ReactiveFormsModule],
  templateUrl: './formulario-pago.component.html',
  styleUrl: './formulario-pago.component.scss'
})
export class FormularioPagoComponent implements OnInit {
  private readonly _formBuilder = inject(FormBuilder)
  private _carritoService = inject(CarritoService)
  private _ordenesService = inject(OrdenesService)
  private readonly _router = inject(Router)
  productosAcomprar:ICarritoProductos[] = []
  step = 1;
  totalCarrito = 0;
  ngOnInit(): void {
    this._carritoService.carritoObservable.subscribe({
      next:(prod)=>{
        this.productosAcomprar = prod
      }
    });
    this._carritoService.totalCarritoObservable.subscribe({
      next:(total)=>{
        this.totalCarrito = total;
      }
    })
  }
  formDireccion = this._formBuilder.nonNullable.group({
      pais: ['',Validators.required],
      ciudad : ['',Validators.required],
      direccion : ['',Validators.required],
  })
  formPago = this._formBuilder.nonNullable.group({
    nombreTarjeta: ['', Validators.required],
    numeroTarjeta: ['', [Validators.required, Validators.pattern(/^\d{16}$/)]],
    expiracion: ['', [Validators.required, Validators.pattern(/^(0[1-9]|1[0-2])\/\d{2}$/)]],
    cvc: ['', [Validators.required, Validators.pattern(/^\d{3,4}$/)]],
  })
  continuar() {
    if (this.formDireccion.valid) {
      this.step = 2;
    } else {
      this.formDireccion.markAllAsTouched();
    }
  }

  finalizar() {
    if (this.formPago.valid) {
       const usuario : IUsuarioCompra = {
            pais:this.formDireccion.value.pais!,
            ciudad:this.formDireccion.value.ciudad!,
            direccion:this.formDireccion.value.direccion!
          }
      this._ordenesService.postOrden(this.productosAcomprar,usuario).subscribe({ 
        next: (respuesta) => {
        console.log(respuesta);
          this._router.navigateByUrl('/home');
          this._carritoService.limpiarCarrito();
        },
        error: (err) => {
          console.error( err);
        }
      });

      

    } else {
      this.formPago.markAllAsTouched();
    }
  }
    

}
