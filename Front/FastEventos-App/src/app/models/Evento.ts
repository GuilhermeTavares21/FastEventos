import { Lote } from "./Lote";
import { Palestrante } from "./Palestrante";
import { RedeSocial } from "./RedeSocial";

export interface Evento {
 id: number;
 local: string;
 dataEvento?: string;
 tema: string;
 qtdPessoas: number;
 telefone: string;
 email: string;
 imagemURL: string;
 lotes: Lote[];
 redesSociais: RedeSocial[];
 palestranteEventos: Palestrante[];
}
