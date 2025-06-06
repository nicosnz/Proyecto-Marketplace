import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';  
import { AutentificacionService } from '../../../services/autentificacion.service';
import { IUsuarioRegistrarse } from '../../../services/models/IUsuarios';

@Component({
  selector: 'app-registrarse',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './registrarse.component.html',
  styleUrl: './registrarse.component.scss'
})
export class RegistrarseComponent {
  private readonly _formBuilder = inject(FormBuilder);
  autentificacion = inject(AutentificacionService);
  private router = inject(Router);
    
  formGroup = this._formBuilder.nonNullable.group({
    nombre : ['',Validators.required],
    email : ['',[Validators.required,Validators.email]],
    contraseña : ['',Validators.required],
    repetirContraseña : ['',Validators.required],
  })

  registrarse(){
    const usuario:IUsuarioRegistrarse = {
      nombre:this.formGroup.value.nombre!,
      email:this.formGroup.value.email!,
      passwordHash:this.formGroup.value.contraseña!
    }
    
    this.autentificacion.registrarse(usuario).subscribe({
      next: (respuesta) => {
        console.log("Usuario registrado:", respuesta);
        this.router.navigateByUrl('/home');
      },
      error: (err) => {
        console.error("Error al registrar:", err);
      }
    });
  
  }
}
