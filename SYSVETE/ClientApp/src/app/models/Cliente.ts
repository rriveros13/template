import { Persona } from "./Persona"

export interface Cliente {
  idCliente: number
  idPersona: number
  idPersonaNavigation: Persona
  ruc: string
  telefono: string
  email: string
  activo: boolean
}


