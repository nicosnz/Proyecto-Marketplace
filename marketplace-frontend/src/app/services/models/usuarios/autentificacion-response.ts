export interface IAutentificacionResponse{
  mensaje:string;
  token:string;
  usuario?: {
    id: number;
    nombre: string;
    email: string;
  };
}