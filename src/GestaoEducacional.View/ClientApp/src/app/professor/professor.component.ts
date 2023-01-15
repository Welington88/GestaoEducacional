import { Component, Input, OnInit, EventEmitter } from '@angular/core';
import { ProfessorService } from '../services/professor.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { IProfessor } from '../models/professor.interface';

@Component({
  selector: 'app-professor',
  templateUrl: './professor.component.html',
  styleUrls: ['./professor.component.css']
})
export class ProfessorComponent implements OnInit {

      @Input() onSubmit = new EventEmitter<IProfessor>();
      @Input() btnText!: string;
      professores : IProfessor[] = [];
      professor : IProfessor = <IProfessor>{};

      formLabel : string = "";
      isEditMode : boolean = false;
      form! : FormGroup;

      constructor(private professorService : ProfessorService,
                  private fb : FormBuilder) {
                this.fb.group({
                  'idProfessor' : [""],
                  'nome' : ["", Validators.required],
                  'dataNascimento' : ["", Validators.required],
                  'salario' : ["", Validators.required],
                });
                this.formLabel = "Adicionar Professor";
        }

      ngOnInit(): void {
        this.getProfessores();
        this.form = new FormGroup({
            idProfessor: new FormControl(''),
            nome: new FormControl('', [Validators.required]),
            dataNascimento: new FormControl('',[Validators.required]),
            salario: new FormControl('',[Validators.required]),
        });
        
      }
      
      get matriculaAluno(){
        return this.form.get("idProfessor")!;
      }

      get nome (){
        return this.form.get("nome")!;
      }

      get dataNascimento (){
        return this.form.get("dataNascimento")!;
      }

      get salario (){
        return this.form.get("salario")!;
      }

      private getProfessores(){
        
          this.professorService.getProfessores().subscribe(
              data => { this.professores = data,
                this.professores.forEach(p => {
                  p.dataNascimento = p.dataNascimento.substring(0,10),
                  p.salario = parseFloat(p.salario.toFixed(2));
                });
              },
              error => alert(error),
              () => console.log(this.professores)
          );
      }

      submit() {
          if (this.form.invalid) {
            return;
          }

          this.professor.nome = this.form.controls['nome'].value;
          this.professor.dataNascimento = this.form.controls['dataNascimento'].value;
          this.professor.salario = this.form.controls['salario'].value;
          if(!(this.isEditMode)){
              this.professorService.addProfessor(this.professor)
              .subscribe(resp =>{
                      this.getProfessores(),
                      this.form.reset()
              });
          } else {
              this.professorService.editProfessor(this.professor)
              .subscribe(resp =>{
                      this.getProfessores(),
                      this.form.reset()
              });
              this.cancel();
          }
          this.form.dirty;
      }

      async send(event : any) {
      }; 

      edit(professor : IProfessor) : void {
          this.formLabel = "Editar Professor";
          this.isEditMode = true;
          this.professor = professor;
          this.form.get("nome")?.setValue(professor.nome);
          this.form.get("dataNascimento")?.setValue(professor.dataNascimento);
          this.form.get("salario")?.setValue(professor.salario);
      };

      delete(professor : IProfessor): void { 
          if(confirm("Deseja excluir este Professor?")) {
            this.professorService.deleteProfessor(professor)
                .subscribe(response => {
                      this.getProfessores();
                      this.form.reset();
                });
          }
      };
      cancel() : void{
          this.formLabel = "Adicionar Professor";
          this.isEditMode = false;
          this.professor = <IProfessor>{};
          this.form.get("nome")?.setValue('');
          this.form.get("dataNascimento")?.setValue('');
          this.form.get("salario")?.setValue('');
          this.form.dirty;
      };
}
