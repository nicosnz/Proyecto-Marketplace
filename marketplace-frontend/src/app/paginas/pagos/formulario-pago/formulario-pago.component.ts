import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CarritoService } from '../../../services/carrito.service';
import { ICarritoProductos, IProducto2 } from '../../../services/models/IProductos';

@Component({
  selector: 'app-formulario-pago',
  imports: [NavbarComponent,ReactiveFormsModule],
  templateUrl: './formulario-pago.component.html',
  styleUrl: './formulario-pago.component.scss'
})
export class FormularioPagoComponent implements OnInit {
  private readonly _formBuilder = inject(FormBuilder)
  private _carritoService = inject(CarritoService)
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
      estado : ['',Validators.required],
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
      console.log(this.formDireccion.value, this.formPago.value);
    } else {
      this.formPago.markAllAsTouched();
    }
  }
    

}
