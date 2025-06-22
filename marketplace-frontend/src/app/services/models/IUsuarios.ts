export interface IUsuarioLogin{
    email:string;
    passwordHash:string;
}
export interface IUsuarioRegistrarse{
    nombre:string;
    email:string;
    passwordHash:string;
}
export interface IUsuarioCompra{
    pais:string;
    ciudad:string;
    direccion:String;
}
export interface IUsuarioInfo{
    personaId:string;
    nombre:string;
    email:string;
    fechaRegistro:Date;
}