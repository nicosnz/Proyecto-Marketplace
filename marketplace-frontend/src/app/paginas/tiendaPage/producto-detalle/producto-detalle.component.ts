import { Component, inject, Input, OnInit } from '@angular/core';
import { IProducto, IProducto2 } from '../../../services/models/IProductos';
import { ProductosApiService } from '../../../services/productos-api.service';
import { routes } from '../../../app.routes';
import { ActivatedRoute } from '@angular/router';
import { NavbarComponent } from "../../navBarPage/navbar/navbar.component";
import { DetalleOrdenComponent } from "../../pagos/detalle-orden/detalle-orden.component";
import { FooterComponent } from "../../footerPage/footer/footer.component";
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-producto-detalle',
  imports: [NavbarComponent,FooterComponent,NgClass],
  templateUrl: './producto-detalle.component.html',
  styleUrl: './producto-detalle.component.scss'
})
export class ProductoDetalleComponent implements OnInit {
  producto?:IProducto;
  private _productsService = inject(ProductosApiService)
  private _route = inject(ActivatedRoute)
  ngOnInit(){
    const id = this._route.snapshot.paramMap.get('id');
    if (id) {
      this._productsService.getProduct(+id).subscribe((prod) => this.producto = prod);
    }

  }
}
