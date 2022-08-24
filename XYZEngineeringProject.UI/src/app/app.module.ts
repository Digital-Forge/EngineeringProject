import { InterceptorService } from './services/interceptor/interceptor.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TaskListComponent } from './components/tasks/task-list/task-list.component';
import { AddTaskComponent } from './components/tasks/add-task/add-task.component';
import { EditTaskComponent } from './components/tasks/edit-task/edit-task.component';
import { NotesListComponent } from './components/notes/notes-list/notes-list.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { LoginComponent } from './components/login/login.component';
import { AppUserListComponent } from './components/appUser/app-user-list/app-user-list.component';
import { AddAppUserComponent } from './components/appUser/add-app-user/add-app-user.component';

@NgModule({
  declarations: [
    AppComponent,
    TaskListComponent,
    AddTaskComponent,
    EditTaskComponent,
    NotesListComponent,
    AuthorizationComponent,
    LoginComponent,
    AppUserListComponent,
    AddAppUserComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass:InterceptorService, multi: true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
