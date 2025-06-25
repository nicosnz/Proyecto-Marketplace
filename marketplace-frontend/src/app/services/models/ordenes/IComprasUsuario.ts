export interface IComprasUsuario{
  ordenId: number;
  fechaOrden?: Date; 
  cantidad: number;
  precioUnitario: number;
  productoId: number;
  nombreProducto?: string;
  descripcion?: string;
  precioProducto: number;
}

