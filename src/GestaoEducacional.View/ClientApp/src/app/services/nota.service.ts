import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of, Subject } from 'rxjs';
import { INota } from '../models/nota.interface';
import { IDisciplina } from '../models/disciplina.interface';
import { IAluno } from '../models/aluno.interface';

@Injectable({
  providedIn: 'root'
})
export class NotaService {

  constructor(private http: HttpClient) { }

  getNotas(){
    return this.http.get("http://localhost:5068/api/Nota/Lista").pipe(map(data=><INota[]>data));
  }

  addNota(nota : INota){
      return this.http.post("http://localhost:5068/api/Nota/Criar",nota);
  }

  editNota(nota : INota){
    return this.http.put(`http://localhost:5068/api/Nota/Atualizar/${nota.idNota}` , nota);
  }

  deleteNota(nota : INota){
    return this.http.delete(`http://localhost:5068/api/Nota/Excluir/${nota.idNota}`);
  }

  getDisciplinas(){
    return this.http.get("http://localhost:5068/api/Disciplina/Lista").pipe(map(data=><IDisciplina[]>data));
  }

  getAlunos(){
    return this.http.get("http://localhost:5068/api/Aluno/Lista").pipe(map(data=><IAluno[]>data));
  }
}
