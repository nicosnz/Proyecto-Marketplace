export interface IProductoEditar{
  productoId:number
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  categoriaId?: number;

}