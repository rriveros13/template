import { Persona } from "./Persona"

export interface Proveedor {
  idProveedor: number
  idPersona: number
  persona: Persona
  ruc: string
  telefono: string
  email: string
}


