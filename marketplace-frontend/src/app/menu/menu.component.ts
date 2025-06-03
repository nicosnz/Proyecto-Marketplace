import { Component, inject, OnInit } from '@angular/core';
import { NavbarComponent } from '../navbar/navbar.component';
import { ProductosApiService } from '../services/productos-api.service';

@Component({
  selector: 'app-menu',
  imports: [NavbarComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent{
  
}
