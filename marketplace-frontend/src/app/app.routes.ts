import { Routes } from '@angular/router';
import { MenuComponent } from './paginas/homePage/menu/menu.component';
import { TiendaComponent } from './paginas/tiendaPage/tienda/tienda.component';
import { FormularioProductoComponent } from './paginas/perfilUsuarioPage/formulario-producto/formulario-producto.component';
import { IniciarSesionComponent } from './paginas/registro-iniciarSesion/iniciar-sesion/iniciar-sesion.component';
import { RegistrarseComponent } from './paginas/registro-iniciarSesion/registrarse/registrarse.component';
import { PerfilUsuarioComponent } from './paginas/perfilUsuarioPage/perfil-usuario/perfil-usuario.component';
import { ProductoDetalleComponent } from './paginas/tiendaPage/producto-detalle/producto-detalle.component';
import { DetalleOrdenComponent } from './paginas/pagos/detalle-orden/detalle-orden.component';
import { FormularioPagoComponent } from './paginas/pagos/formulario-pago/formulario-pago.component';

export const routes: Routes = [
    {path:'home', component:MenuComponent},
    {path:'iniciar-sesion',component:IniciarSesionComponent},
    {path:'registrarse',component:RegistrarseComponent},
    {path:'tienda',component:TiendaComponent},
    {path:'perfil',component:PerfilUsuarioComponent},
    {path:'añadir-formulario',component:FormularioProductoComponent},
    {path:'añadir-formulario/:id',component:FormularioProductoComponent},
    {path:'producto-detalle/:id',component:ProductoDetalleComponent},
    {path:'formulario-pago',component:FormularioPagoComponent},
    {path:'carrito',component:DetalleOrdenComponent},

    {path:'**',redirectTo:'/home',pathMatch:'full'}
];
