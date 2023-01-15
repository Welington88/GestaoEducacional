import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { ServerModule } from '@angular/platform-server';
import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AlunoComponent } from './aluno/aluno.component';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { AlunoService } from './services/aluno.service';
import { CursoService } from './services/curso.service';
import { DisciplinaService } from './services/disciplina.service';
import { NotaService } from './services/nota.service';
import { ProfessorService } from './services/professor.service';

@NgModule({
    imports: [AppModule, ServerModule , ReactiveFormsModule ,ModuleMapLoaderModule],
    bootstrap: [AppComponent],
    providers: [
        AlunoService,
        CursoService,
        DisciplinaService,
        NotaService,
        ProfessorService,
        { provide: 'ORIGIN_URL', useValue: location.origin}
    ]
})

export class AppServerModule { }
