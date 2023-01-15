import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { AlunoService } from '../services/aluno.service';
import { IAluno } from '../models/aluno.interface'; 
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-aluno-component',
  templateUrl: './aluno.component.html',
  styleUrls: ['./aluno.component.css']
})
export class AlunoComponent implements OnInit {
        @Input() onSubmit = new EventEmitter<IAluno>();
        @Input() btnText!: string;
        alunos : IAluno[] = [];
        aluno : IAluno = <IAluno>{};

        formLabel : string = "";
        isEditMode : boolean = false;
        form! : FormGroup;

        constructor(private alunoService : AlunoService,
                    private fb : FormBuilder) {
                  this.fb.group({
                    'matriculaAluno' : [""],
                    'nome' : ["", Validators.required],
                    'dataNascimento' : ["", Validators.required]
                  });
              this.formLabel = "Adicionar Aluno";
          }

        ngOnInit(): void {
          this.getAlunos();
          this.form = new FormGroup({
            matriculaAluno: new FormControl(''),
            nome: new FormControl('', [Validators.required]),
            dataNascimento: new FormControl('',[Validators.required])

          });
          console.log(this.form);
        }
        
        get matriculaAluno(){
          return this.form.get("matriculaAluno")!;
        }

        get nome (){
          return this.form.get("nome")!;
        }

        get dataNascimento (){
          return this.form.get("dataNascimento")!;
        }

        private getAlunos(){
          
            this.alunoService.getAlunos().subscribe(
                data => { this.alunos = data,
                  this.alunos.forEach(a => {
                    a.dataNascimento = a.dataNascimento.substring(0,10)
                  });
                },
                error => alert(error),
                () => console.log(this.alunos)
            );
        }

        submit() {
          if (this.form.invalid) {
            return;
          }

            this.aluno.nome = this.form.controls['nome'].value;
            this.aluno.dataNascimento = this.form.controls['dataNascimento'].value;
            if(!(this.isEditMode)){
                this.alunoService.addAluno(this.aluno)
                .subscribe(resp =>{
                        this.getAlunos(),
                        this.form.reset()
                });
            } else {
                this.alunoService.editAluno(this.aluno)
                .subscribe(resp =>{
                        this.getAlunos(),
                        this.form.reset()
                });
                this.cancel();
            }

            this.form.dirty;
        }

        async send(event : any) {

            this.aluno.nome = this.form.controls['nome'].value;
            this.aluno.dataNascimento = this.form.controls['dataNascimento'].value;
  
            this.alunoService.addAluno(this.aluno)
            .subscribe(resp =>{
                    this.getAlunos(),
                    this.form.reset()
            });
        }; 

        edit(aluno : IAluno) : void {
            this.formLabel = "Editar Aluno";
            this.isEditMode = true;
            this.aluno = aluno;
            this.form.get("nome")?.setValue(aluno.nome);
            this.form.get("dataNascimento")?.setValue(aluno.dataNascimento);
         };
        delete(aluno : IAluno): void { 
            if(confirm("Deseja excluir este Aluno?")) {
              this.alunoService.deleteAluno(aluno)
                  .subscribe(response => {
                        this.getAlunos();
                        this.form.reset();
                  });
            }
        };
        cancel() : void{
            this.formLabel = "Adicionar Aluno";
            this.isEditMode = false;
            this.aluno = <IAluno>{};
            this.form.get("nome")?.setValue('');
            this.form.get("dataNascimento")?.setValue('');
            this.form.dirty;
        };
}