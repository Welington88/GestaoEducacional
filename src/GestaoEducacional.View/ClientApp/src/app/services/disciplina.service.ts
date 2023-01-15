import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of, Subject } from 'rxjs';
import { ICurso } from '../models/curso.interface';
import { IProfessor } from '../models/professor.interface';
import { IDisciplina } from '../models/disciplina.interface';

@Injectable({
  providedIn: 'root'
})
export class DisciplinaService {

  constructor(private http: HttpClient) { }

  getDisciplinas(){
    return this.http.get("http://localhost:5068/api/Disciplina/Lista").pipe(map(data=><IDisciplina[]>data));
  }

  addDisciplina(disciplina : IDisciplina){
      return this.http.post("http://localhost:5068/api/Disciplina/Criar",disciplina);
  }

  editDisciplina(disciplina : IDisciplina){
    return this.http.put(`http://localhost:5068/api/Disciplina/Atualizar/${disciplina.idDisciplina}` , disciplina);
  }

  deleteDisciplina(disciplina : IDisciplina){
    return this.http.delete(`http://localhost:5068/api/Disciplina/Excluir/${disciplina.idDisciplina}`);
  }

  getCursos(){
    return this.http.get("http://localhost:5068/api/Curso/Lista").pipe(map(data=><ICurso[]>data));
  }

  getProfessores(){
    return this.http.get("http://localhost:5068/api/Professor/Lista").pipe(map(data=><IProfessor[]>data));
  }
}
