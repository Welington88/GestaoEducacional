import { Component, EventEmitter, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ICurso } from '../models/curso.interface';
import { CursoService } from '../services/curso.service';

@Component({
  selector: 'app-curso',
  templateUrl: './curso.component.html',
  styleUrls: ['./curso.component.css']
})

export class CursoComponent implements OnInit {
  @Input() onSubmit = new EventEmitter<ICurso>();
  @Input() btnText!: string;
  cursos : ICurso[] = [];
  curso : ICurso = <ICurso>{};

  formLabel : string = "";
  isEditMode : boolean = false;
  form! : FormGroup;

  constructor(private cursoService : CursoService,
              private fb : FormBuilder) {
            this.fb.group({
              'idCurso' : [""],
              'descricaoCurso' : ["", Validators.required]
            });
        this.formLabel = "Adicionar Curso";
    }

  ngOnInit(): void {
    this.getCursos();
    this.form = new FormGroup({
      idCurso: new FormControl(''),
      descricaoCurso: new FormControl('', [Validators.required]),

    });
  }
  
  get idCurso(){
    return this.form.get("idCurso")!;
  }

  get descricaoCurso (){
    return this.form.get("descricaoCurso")!;
  }

  private getCursos(){
    
      this.cursoService.getCursos().subscribe(
          data => { this.cursos = data
            this.cursos.forEach(c => {
              c.mediaCurso = parseFloat(c.mediaCurso.toFixed(2));
            });
          },
          error => alert(error),
          () => console.log(this.cursos)
      );
  }

  submit() {
    if (this.form.invalid) {
      return;
    }

      this.curso.descricaoCurso = this.form.controls['descricaoCurso'].value;
      
      if(!(this.isEditMode)){
          this.cursoService.addCurso(this.curso)
          .subscribe(resp =>{
                  this.getCursos(),
                  this.form.reset()
          });
      } else {
          this.cursoService.editCurso(this.curso)
          .subscribe(resp =>{
                  this.getCursos(),
                  this.form.reset()
          });
          this.cancel();
      }

      this.form.dirty;
  }

  async send(event : any) {
  }; 

  edit(curso : ICurso) : void {
      this.formLabel = "Editar Curso";
      this.isEditMode = true;
      this.curso = curso;
      this.form.get("descricaoCurso")?.setValue(curso.descricaoCurso);
   };

  delete(curso : ICurso): void { 
      if(confirm("Deseja excluir este Curso?")) {
        this.cursoService.deleteCurso(curso)
            .subscribe(response => {
                  this.getCursos();
                  this.form.reset();
            });
      }
  };
  cancel() : void{
      this.formLabel = "Adicionar Curso";
      this.isEditMode = false;
      this.curso = <ICurso>{};
      this.form.get("descricaoCurso")?.setValue(' ');
      this.form.dirty;
  };
}