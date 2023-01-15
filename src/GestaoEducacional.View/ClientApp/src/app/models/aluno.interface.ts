import { INotasAluno } from "./notasaluno.interface";

export interface IAluno{
        matriculaAluno: number,
        nome: string,
        dataNascimento: string,
        notas: INotasAluno[]
}