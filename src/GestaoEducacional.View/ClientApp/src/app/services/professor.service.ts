import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable, of, Subject } from 'rxjs';
import { IProfessor } from '../models/professor.interface';


@Injectable({
  providedIn: 'root'
})
export class ProfessorService {

  constructor(private http: HttpClient) { }

  getProfessores(){
    return this.http.get("http://localhost:5068/api/Professor/Lista").pipe(map(data=><IProfessor[]>data));
  }

  addProfessor(professor : IProfessor){
      return this.http.post("http://localhost:5068/api/Professor/Criar",professor);
  }

  editProfessor(professor : IProfessor){
    return this.http.put(`http://localhost:5068/api/Professor/Atualizar/${professor.idProfessor}` , professor);
  }

  deleteProfessor(professor : IProfessor){
    return this.http.delete(`http://localhost:5068/api/Professor/Excluir/${professor.idProfessor}`);
  }
}
