import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';  
import { IUsuarioRegistrarse } from '../../../services/models/usuarios/IUsuarioRegistrarse';
import { AutentificacionApiService } from '../../../services/autentificacion-api.service';


@Component({
  selector: 'app-registrarse',
  imports: [CommonModule,ReactiveFormsModule],
  templateUrl: './registrarse.component.html'
})
export class RegistrarseComponent {
  private readonly _formBuilder = inject(FormBuilder);
  autentificacion = inject(AutentificacionApiService);
  private router = inject(Router);
  mensajeError:string|null = null
  mensajeExito:string|null = null


    
  formGroup = this._formBuilder.nonNullable.group({
    nombre : ['',Validators.required],
    email : ['',[Validators.required,Validators.email]],
    password : ['',Validators.required],
    repetirPassword : ['',Validators.required],
  })

  registrarse(){
    const usuario:IUsuarioRegistrarse = {
      nombre:this.formGroup.value.nombre!,
      email:this.formGroup.value.email!,
      passwordHash:this.formGroup.value.password!
    }
    
    this.autentificacion.registrarse(usuario).subscribe({
      next: (respuesta) => {
        this.mensajeError = null;
        this.mensajeExito = respuesta.mensaje;
        this.formGroup.reset();
        setTimeout(() => {
          this.mensajeExito = null;
          this.router.navigateByUrl('/home');
        }, 1000);
      },
      error: (err) => {
        this.mensajeError = err.error.mensaje;
        this.formGroup.reset();

      }
    });
  
  }
}
