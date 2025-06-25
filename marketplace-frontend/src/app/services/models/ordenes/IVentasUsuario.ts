export interface IVentasUsuario{
  productoID: number;
  nombreProducto?: string;
  descripcion?: string;
  totalVendido: number;
  totalIngresos: number;
  cantidadOrdenes: number;
}