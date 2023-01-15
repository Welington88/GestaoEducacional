import { IDisciplina } from "./disciplina.interface";
import { IAluno } from "./aluno.interface";

export interface INota {
        idNota: number,
        valorNota: number,
        disciplina: IDisciplina,
        aluno : IAluno
}
