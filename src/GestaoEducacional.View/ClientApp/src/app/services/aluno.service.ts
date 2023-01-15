import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of, Subject } from 'rxjs';
import { IAluno } from '../models/aluno.interface'

@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  constructor(private http: HttpClient) {}

    getAlunos(){
        return this.http.get("http://localhost:5068/api/Aluno/Lista").pipe(map(data=><IAluno[]>data));
    }

    addAluno(aluno : IAluno){
        return this.http.post("http://localhost:5068/api/Aluno/Criar",aluno);
    }

    editAluno(aluno : IAluno){
      return this.http.put(`http://localhost:5068/api/Aluno/Atualizar/${aluno.matriculaAluno}` , aluno);
    }

    deleteAluno(aluno : IAluno){
      return this.http.delete(`http://localhost:5068/api/Aluno/Excluir/${aluno.matriculaAluno}`);
    }
}
