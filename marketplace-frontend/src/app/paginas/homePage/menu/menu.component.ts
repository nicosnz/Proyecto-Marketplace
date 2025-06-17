import { Component, inject, OnInit } from '@angular/core';
import { FooterComponent } from '../../footerPage/footer/footer.component';
import {  NavbarComponent } from '../../navBarPage/navbar/navbar.component';

@Component({
  selector: 'app-menu',
  imports: [NavbarComponent,FooterComponent],
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})
export class MenuComponent{
  
}
