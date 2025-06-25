import { Component, inject } from '@angular/core';
import { Router} from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IUsuarioLogin } from '../../../services/models/usuarios/IUsuarioLogin';
import { AutentificacionApiService } from '../../../services/autentificacion-api.service';

@Component({
  selector: 'app-iniciar-sesion',
  imports: [ReactiveFormsModule],
  templateUrl: './iniciar-sesion.component.html'
})
export class IniciarSesionComponent {
  autentificacion = inject(AutentificacionApiService);
  private readonly _formBuilder = inject(FormBuilder);
  private router = inject(Router);
  mensajeError:string|null = null
  mensajeExito:string|null = null
    
  formGroup = this._formBuilder.nonNullable.group({
    email : ['',[Validators.required,Validators.email]],
    password : ['',Validators.required],
  })
  
  login(){
    
    const usuario : IUsuarioLogin = {
      email:this.formGroup.value.email!,
      passwordHash:this.formGroup.value.password!
    }
    this.autentificacion.login(usuario).subscribe({
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
