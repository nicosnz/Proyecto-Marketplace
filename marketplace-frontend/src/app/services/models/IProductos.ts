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
export interface IProducto2{
    productoId:number;
    productoNombre:string;
    descripcion:string;
    precio:number;
    stock:number;
    activo:boolean;
    categoriaId:number;
    categoriaNombre:string;
    vendedorId:number;
    vendedorNombre:string;

}
export interface IProductoAñadir{
  nombre: string;
  descripcion?: string;
  precio: number;
  stock: number;
  categoriaId?: number;
}

