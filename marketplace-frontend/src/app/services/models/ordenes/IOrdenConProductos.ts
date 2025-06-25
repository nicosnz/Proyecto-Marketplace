import { IComprasUsuario } from "./IComprasUsuario";

export interface OrdenConProductos  {
  ordenId: number;
  fechaOrden?: Date;
  productos: IComprasUsuario[];
  total:number
};

