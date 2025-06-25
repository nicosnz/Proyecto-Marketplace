import { IProducto } from './IProducto';
export interface ICarritoProductos{
  producto:IProducto;
  cantidad:number;
  total:number;
}