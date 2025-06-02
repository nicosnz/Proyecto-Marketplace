import { Routes } from '@angular/router';
import { MenuComponent } from './menu/menu.component';
import { IniciarSesionComponent } from './iniciar-sesion/iniciar-sesion.component';
import { RegistrarseComponent } from './registrarse/registrarse.component';

export const routes: Routes = [
    {path:'home', component:MenuComponent},
    {path:'iniciar-sesion',component:IniciarSesionComponent},
    {path:'registrarse',component:RegistrarseComponent},
    {path:'**',redirectTo:'/home',pathMatch:'full'}
];
