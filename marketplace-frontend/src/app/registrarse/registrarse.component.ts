import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';  

@Component({
  selector: 'app-registrarse',
  imports: [RouterLink,CommonModule,ReactiveFormsModule],
  templateUrl: './registrarse.component.html',
  styleUrl: './registrarse.component.scss'
})
export class RegistrarseComponent {
  private readonly _formBuilder = inject(FormBuilder);
  formGroup = this._formBuilder.nonNullable.group({
    nombre : ['',Validators.required],
    email : ['',[Validators.required,Validators.email]],
    contraseña : ['',Validators.required],
    repetirContraseña : ['',Validators.required],
  })

  clickeo():void{
    console.log(this.formGroup.valid)
  }
}
