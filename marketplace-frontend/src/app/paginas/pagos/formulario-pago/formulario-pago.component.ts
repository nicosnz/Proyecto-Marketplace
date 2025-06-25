import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CarritoService } from '../../../services/carrito.service';
import { Router } from '@angular/router';
import { OrdenesServiceApiService } from '../../../services/ordenes-api.service';
import { ICarritoProductos } from '../../../services/models/productos/ICarritoProductos';
import { IUsuarioCompra } from '../../../services/models/usuarios/IUsuarioCompra';

@Component({
  selector: 'app-formulario-pago',
  imports: [NavbarComponent,ReactiveFormsModule],
  templateUrl: './formulario-pago.component.html'
})
export class FormularioPagoComponent implements OnInit {
  private readonly _formBuilder = inject(FormBuilder)
  private _carritoService = inject(CarritoService)
  private _ordenesService = inject(OrdenesServiceApiService)
  private readonly _router = inject(Router)
  ciudades:string[] = ["Santa Cruz de la Sierra","Cochabamba","Tarija","Potosi","La Paz","Pando","Beni","Oruro","Chuquisaca"]
  productosAcomprar:ICarritoProductos[] = []
  step = 1;
  totalCarrito = 0;
  mensajeError:string|null = null
  mensajeExito:string|null = null
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
      pais: ['Bolivia',Validators.required],
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
          this.mensajeError = null;
          this.mensajeExito = "Compra Exitosa";
          setTimeout(() => {
            this.mensajeExito = null;
            this._router.navigateByUrl('/home');
            this._carritoService.limpiarCarrito();
          }, 1000);
        },
        error: (err) => {
          console.error( err);
          this.mensajeError = err.error.mensaje;
        }
      });

      

    } else {
      this.formPago.markAllAsTouched();
    }
  }
    

}
