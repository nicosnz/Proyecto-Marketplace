import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../../navBarPage/navbar/navbar.component';
import { ProductosApiService } from '../../../services/productos-api.service';
import { FooterComponent } from '../../footerPage/footer/footer.component';

@Component({
  selector: 'app-menu',
  imports: [NavbarComponent,FooterComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent{
  
}
