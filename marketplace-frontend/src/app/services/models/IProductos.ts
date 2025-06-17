export interface IProducto{
    productoId:number;
    nombre:string;
    descripcion:string;
    precio:number;
    stock:number;
    activo:boolean;
    categoriaId:number;
    categoriaNombre:string;
    vendedorId:number;
    vendedorNombre:string;

}
export interface IProducto2 {
  productoId: number;
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  categoriaId: number;
  vendedorId: number;
  imagenBase64: string | null;
}
export interface IProductoAñadir{
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  categoriaId?: number;
}
export interface IProductoEditar{
  productoId:number
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  categoriaId?: number;

}

export interface ICarritoProductos{
  producto:IProducto2;
  cantidad:number;
  total:number;
}