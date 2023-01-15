import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of, Subject } from 'rxjs';
import { ICurso } from '../models/curso.interface';

@Injectable({
  providedIn: 'root'
})
export class CursoService {

  constructor(private http: HttpClient) {}

    getCursos(){
        return this.http.get("http://localhost:5068/api/Curso/Lista").pipe(map(data=><ICurso[]>data));
    }

    addCurso(curso : ICurso){
        return this.http.post("http://localhost:5068/api/Curso/Criar",curso);
    }

    editCurso(curso : ICurso){
      return this.http.put(`http://localhost:5068/api/Curso/Atualizar/${curso.idCurso}` , curso);
    }

    deleteCurso(curso : ICurso){
      return this.http.delete(`http://localhost:5068/api/Curso/Excluir/${curso.idCurso}`);
    }
}
