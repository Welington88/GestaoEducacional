import { IAluno } from "./aluno.interface"
import { ICurso } from "./curso.interface"
import { IProfessor } from "./professor.interface"

export interface IDisciplina {    
        idDisciplina: number,
        descricaoDisciplina: string,
        quantidadeAlunos: number,
        professor : IProfessor,
        curso : ICurso,
        alunos: IAluno[]
}