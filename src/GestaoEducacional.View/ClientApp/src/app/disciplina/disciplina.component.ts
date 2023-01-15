import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ICurso } from '../models/curso.interface';
import { IDisciplina } from '../models/disciplina.interface';
import { IProfessor } from '../models/professor.interface';
import { DisciplinaService } from '../services/disciplina.service';

@Component({
  selector: 'app-disciplina',
  templateUrl: './disciplina.component.html',
  styleUrls: ['./disciplina.component.css']
})
export class DisciplinaComponent implements OnInit {

  @Input() onSubmit = new EventEmitter<IDisciplina>();
  @Input() btnText!: string;
  disciplinas : IDisciplina[] = [];
  disciplina : IDisciplina = <IDisciplina>{};
  cursos : ICurso[] = [];
  professores : IProfessor[] = [];
  formLabel : string = "";
  isEditMode : boolean = false;
  form! : FormGroup;
  
  constructor(private disciplinaService : DisciplinaService,
              private fb : FormBuilder) {
            this.fb.group({
              'idDisciplina' : [""],
              'descricaoDisciplina' : ["", Validators.required],
              'professor' : ["", Validators.required],
              'curso' : ["", Validators.required]
            });

        this.formLabel = "Adicionar Disciplina";
    }

    ngOnInit(): void {
      this.getDisciplinas();
      this.getCursos();
      this.getProfessores();
      this.form = new FormGroup({
        idDisciplina: new FormControl(''),
        descricaoDisciplina: new FormControl('', [Validators.required]),
        professor: new FormControl('',[Validators.required]),
        curso: new FormControl('')

      });
      console.log(this.form);
    }
    
    get idDisciplina(){
      return this.form.get("idDisciplina")!;
    }

    get descricaoDisciplina (){
      return this.form.get("descricaoDisciplina")!;
    }

    get professor (){
      return this.form.get("professor")!;
    }

    get curso (){
      return this.form.get("curso")!;
    }

    private getDisciplinas(){
      
        this.disciplinaService.getDisciplinas().subscribe(
            data => { this.disciplinas = data},
            error => alert(error),
            () => console.log(this.disciplinas)
        );
    }

    private getCursos(){
      
      this.disciplinaService.getCursos().subscribe(
          data => { this.cursos = data},
          error => alert(error),
          () => console.log(this.cursos)
      );
    }

    private getProfessores(){
      
      this.disciplinaService.getProfessores().subscribe(
          data => { this.professores = data},
          error => alert(error),
          () => console.log(this.professores)
      );
    }

    submit() {
        if (this.form.invalid) {
          return;
        }

        this.disciplina.descricaoDisciplina = this.form.controls['descricaoDisciplina'].value;
        
        this.cursos.forEach(curso => {
          if(curso.descricaoCurso == this.form.controls['curso'].value) {
            this.disciplina.curso = curso;  
          }
        });
        
        this.professores.forEach(professor =>{
            if (professor.nome == this.form.controls['professor'].value) {
              this.disciplina.professor = professor;  
            }
        });
        
        if(!(this.isEditMode)){
            this.disciplinaService.addDisciplina(this.disciplina)
            .subscribe(resp =>{
                    this.getDisciplinas(),
                    this.form.reset()
            });
        } else {
            this.disciplinaService.editDisciplina(this.disciplina)
            .subscribe(resp =>{
                    this.getDisciplinas(),
                    this.form.reset()
            });
            this.cancel();
        }

        this.form.dirty;
        
    }

    async send(event : any) {
    }; 

    edit(disciplina : IDisciplina) : void {
        this.formLabel = "Editar Disciplina";
        this.isEditMode = true;
        this.disciplina = disciplina;
        this.form.get("descricaoDisciplina")?.setValue(disciplina.descricaoDisciplina);
        this.form.get("curso")?.setValue(disciplina.curso.descricaoCurso);
        this.form.get("professor")?.setValue(disciplina.professor.nome);
    };

    delete(disciplina : IDisciplina): void { 
        if(confirm("Deseja excluir este Disciplina?")) {
          this.disciplinaService.deleteDisciplina(disciplina)
              .subscribe(response => {
                    this.getDisciplinas();
                    this.form.reset();
              });
        }
    };
    cancel() : void{
        this.formLabel = "Adicionar Disciplina";
        this.isEditMode = false;
        this.disciplina = <IDisciplina>{};
        this.form.get("descricaoDisciplina")?.setValue('');
        this.form.get("curso")?.setValue('');
        this.form.get("professor")?.setValue('');

        this.form.dirty;
    };
}
