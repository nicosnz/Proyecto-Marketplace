// export interface IProductos{
//     productoId:number;
//     nombre:string;
//     descripcion:string;
//     precio:number;
//     stock:number;
//     activo:boolean;
//     categoriaId:number;
//     categoriaNombre:string;
//     vendedorId:number;
//     vendedorNombre:string;

import { IComentarios } from "../IComentarios";

// }
export interface IProducto {
  productoId: number;
  nombre: string;
  descripcion: string;
  precio: number;
  stock: number;
  categoriaId: number;
  vendedorId: number;
  nombreVendedor: string,
  imagenBase64: string | null;
  comentarios:IComentarios[];
}



