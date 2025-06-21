export interface IMisCompras{
  ordenId: number;
  fechaOrden?: Date; 
  cantidad: number;
  precioUnitario: number;
  productoId: number;
  nombreProducto?: string;
  descripcion?: string;
  precioProducto: number;
}
export interface IMisVentas{
  productoID: number;
  nombreProducto?: string;
  descripcion?: string;
  totalVendido: number;
  totalIngresos: number;
  cantidadOrdenes: number;
}
export interface OrdenConProductos  {
  ordenId: number;
  fechaOrden?: Date;
  productos: IMisCompras[];
  total:number
};

