import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { AlunoComponent } from './aluno/aluno.component';
import { AlunoService } from './services/aluno.service';
import { CursoComponent } from './curso/curso.component';
import { CursoService } from './services/curso.service';
import { ProfessorComponent } from './professor/professor.component';
import { ProfessorService } from './services/professor.service';
import { DisciplinaComponent } from './disciplina/disciplina.component';
import { DisciplinaService } from './services/disciplina.service';
import { NotaService } from "./services/nota.service";
import { NotaComponent } from './nota/nota.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    AlunoComponent,
    CursoComponent,
    DisciplinaComponent,
    NotaComponent,
    ProfessorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'aluno', component: AlunoComponent },
      { path: 'curso', component: CursoComponent },
      { path: 'disciplina', component: DisciplinaComponent },
      { path: 'nota', component: NotaComponent },
      { path: 'professor', component: ProfessorComponent },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
    ])
  ],
  providers: [AlunoService, CursoService, DisciplinaService, NotaService ,ProfessorService],
  bootstrap: [AppComponent]
})
export class AppModule { }
