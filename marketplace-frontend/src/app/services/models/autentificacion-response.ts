export interface IAutentificacionResponse{
    token:string;
    usuario?: {
    id: number;
    nombre: string;
    email: string;
  };
}