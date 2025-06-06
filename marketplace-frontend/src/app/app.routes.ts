import { Routes } from '@angular/router';
import { MenuComponent } from './paginas/homePage/menu/menu.component';
import { TiendaComponent } from './paginas/tiendaPage/tienda/tienda.component';
import { FormularioProductoComponent } from './paginas/perfilUsuarioPage/formulario-producto/formulario-producto.component';
import { IniciarSesionComponent } from './paginas/registro-iniciarSesion/iniciar-sesion/iniciar-sesion.component';
import { RegistrarseComponent } from './paginas/registro-iniciarSesion/registrarse/registrarse.component';
import { PerfilUsuarioComponent } from './paginas/perfilUsuarioPage/perfil-usuario/perfil-usuario.component';

export const routes: Routes = [
    {path:'home', component:MenuComponent},
    {path:'iniciar-sesion',component:IniciarSesionComponent},
    {path:'registrarse',component:RegistrarseComponent},
    {path:'tienda',component:TiendaComponent},
    {path:'perfil',component:PerfilUsuarioComponent},
    {path:'añadir-formulario',component:FormularioProductoComponent},
    {path:'**',redirectTo:'/home',pathMatch:'full'}
];
