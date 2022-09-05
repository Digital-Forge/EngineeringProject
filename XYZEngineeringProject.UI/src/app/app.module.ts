import { InterceptorService } from './services/interceptor/interceptor.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskListComponent } from './components/tasks/task-list/task-list.component';
import { AddTaskComponent } from './components/tasks/add-task/add-task.component';
import { EditTaskComponent } from './components/tasks/edit-task/edit-task.component';
import { NotesListComponent } from './components/notes/notes-list/notes-list.component';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { ClientComponent } from './components/client/client.component';
import { ClientFormComponent } from './components/client/form/client-form.component';
import { ClientViewComponent } from './components/client/view/client-view.component';
import { ClientIndexComponent } from './components/client/index/client-index.component';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { AppUserListComponent } from './components/appUser/app-user-list/app-user-list.component';
import { AddAppUserComponent } from './components/appUser/add-app-user/add-app-user.component';
import { EditAppUserComponent } from './components/appUser/edit-app-user/edit-app-user.component';
import { ClientFiltersComponent } from './components/client/filters/filters.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    AddTaskComponent,
    EditTaskComponent,
    NotesListComponent,
    AuthorizationComponent,
    LoginComponent,
    ClientComponent,
    ClientFormComponent,
    ClientViewComponent,
    ClientIndexComponent,
    ClientFiltersComponent,
    AppUserListComponent,
    AddAppUserComponent,
    EditAppUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    HttpClientModule,
    TranslateModule.forRoot({
        loader: {
            provide: TranslateLoader,
            useFactory: HttpLoaderFactory,
            deps: [HttpClient]
        }
    })
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass:InterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}