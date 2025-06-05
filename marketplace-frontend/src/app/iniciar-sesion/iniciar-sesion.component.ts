import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { AutentificacionService } from '../services/autentificacion.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { IUsuarioLogin } from '../services/models/IUsuarios';

@Component({
  selector: 'app-iniciar-sesion',
  imports: [ReactiveFormsModule],
  templateUrl: './iniciar-sesion.component.html',
  styleUrl: './iniciar-sesion.component.scss'
})
export class IniciarSesionComponent {
  autentificacion = inject(AutentificacionService);
  private readonly _formBuilder = inject(FormBuilder);
  private router = inject(Router);
    
  formGroup = this._formBuilder.nonNullable.group({
    email : ['',[Validators.required,Validators.email]],
    contraseña : ['',Validators.required],
  })
  
  login(){
    
    const usuario : IUsuarioLogin = {
      email:this.formGroup.value.email!,
      passwordHash:this.formGroup.value.contraseña!
    }
    this.autentificacion.login(usuario).subscribe({
      next: (respuesta) => {
        console.log("Usuario logeado:", respuesta);
        this.router.navigateByUrl('/home');
      },
      error: (err) => {
        console.error("Error al logear:", err);
      }
    });
  
  }
}
