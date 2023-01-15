import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { IAluno } from '../models/aluno.interface';
import { IDisciplina } from '../models/disciplina.interface';
import { INota } from '../models/nota.interface';
import { NotaService } from '../services/nota.service';

@Component({
  selector: 'app-nota',
  templateUrl: './nota.component.html',
  styleUrls: ['./nota.component.css']
})
export class NotaComponent implements OnInit {

  @Input() onSubmit = new EventEmitter<INota>();
  @Input() btnText!: string;
  notas : INota[] = [];
  nota : INota = <INota>{};
  alunos : IAluno[] = [];
  disciplinas : IDisciplina[] = [];
  formLabel : string = "";
  isEditMode : boolean = false;
  form! : FormGroup;
  
  constructor(private notaService : NotaService,
              private fb : FormBuilder) {
            this.fb.group({
              'idNota' : [""],
              'valorNota' : ["", Validators.required],
              'disciplina' : ["", Validators.required],
              'aluno' : ["", Validators.required]
            });

        this.formLabel = "Adicionar Nota";
    }

    ngOnInit(): void {
      this.getNotas();
      this.getAlunos();
      this.getDisciplinas();
      this.form = new FormGroup({
        idNota: new FormControl(''),
        valorNota: new FormControl('', [Validators.required]),
        disciplina: new FormControl(''),
        aluno: new FormControl('')
      });
    }
    
    get idNota(){
      return this.form.get("idNota")!;
    }

    get valorNota (){
      return this.form.get("valorNota")!;
    }

    get disciplina (){
      return this.form.get("disciplina")!;
    }

    get aluno (){
      return this.form.get("aluno")!;
    }

    private getNotas(){
      
        this.notaService.getNotas().subscribe(
            data => { this.notas = data,
                data.forEach(n => {
                  n.valorNota = parseFloat(n.valorNota.toFixed(2));
                });
            },
            error => alert(error),
            () => console.log(this.disciplinas)
        );
    }

    private getDisciplinas(){
      
      this.notaService.getDisciplinas().subscribe(
          data => { this.disciplinas = data},
          error => alert(error),
          () => console.log(this.disciplinas)
      );
    }

    private getAlunos(){
      
      this.notaService.getAlunos().subscribe(
          data => { this.alunos = data},
          error => alert(error),
          () => console.log(this.alunos)
      );
    }

    submit() {
        if (this.form.invalid) {
          return;
        }

        this.nota.valorNota = this.form.controls['valorNota'].value;
        
        this.disciplinas.forEach(d => {
          if(d.descricaoDisciplina == this.form.controls['disciplina'].value) {
            this.nota.disciplina = d;  
          }
        });
        
        this.alunos.forEach( a =>{
            if (a.nome == this.form.controls['aluno'].value) {
              this.nota.aluno = a;  
            }
        });

        if(!(this.isEditMode)){
            this.notaService.addNota(this.nota)
            .subscribe(resp =>{
                    this.getNotas(),
                    this.form.reset()
            });
        } else {
            this.notaService.editNota(this.nota)
            .subscribe(resp =>{
                    this.getDisciplinas(),
                    this.form.reset()
            });
            this.cancel();
        }
    }

    async send(event : any) {
    }; 

    edit(nota : INota) : void {
        this.formLabel = "Editar Disciplina";
        this.isEditMode = true;
        this.nota = nota;
        this.form.get("valorNota")?.setValue(nota.valorNota);
        this.form.get("disciplina")?.setValue(nota.disciplina.descricaoDisciplina);
        this.form.get("aluno")?.setValue(nota.aluno.nome);
    };

    delete(nota : INota): void { 
        if(confirm("Deseja excluir este Nota?")) {
          this.notaService.deleteNota(nota)
              .subscribe(response => {
                    this.getDisciplinas();
                    this.form.reset();
              });
        }
    };

    cancel() : void{
        this.formLabel = "Adicionar Nota";
        this.isEditMode = false;
        this.nota = <INota>{};
        this.form.get("valorNota")?.setValue('');
        this.form.get("disciplina")?.setValue('');
        this.form.get("aluno")?.setValue('');

        this.form.dirty;
    };
}
