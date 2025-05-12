export interface Usuario {
  idUsuario: number;
  idRol: number;
  nombre: string;
  alias: string;
  contrasena: boolean;
  activo: boolean;
}

export interface UsuarioLogueado {
  idUsuario: number;
  nombre: string;
  alias: String;
  token: string | null;
}


