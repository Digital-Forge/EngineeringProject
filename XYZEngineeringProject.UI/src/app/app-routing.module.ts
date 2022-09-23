import { NoteFormComponent } from './components/note/note-form/note-form.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { NoteComponent } from './components/note/note.component';
import { UserTaskFormComponent } from './components/user-task/userTaskForm/form.component';
import { ClientIndexComponent } from './components/client/clientIndex/client-index.component';
import { ClientFormComponent } from './components/client/clientForm/client-form.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { EditAppUserComponent } from './components/appUser/edit-app-user/edit-app-user.component';
import { AddAppUserComponent } from './components/appUser/add-app-user/add-app-user.component';
import { AppUserListComponent } from './components/appUser/app-user-list/app-user-list.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UserTaskIndexComponent } from './components/user-task/userTaskIndex/index.component';
import { UserTaskComponent } from './components/user-task/user-task.component';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'tasks',
    component: UserTaskComponent,
    children: [
      {
        path: '', component: UserTaskIndexComponent },
      {
        path: 'add', component: UserTaskFormComponent },
      {
        path: 'edit/:id', component: UserTaskFormComponent },
    ]
  },
  {
    path: 'clients',
    component: ClientIndexComponent
  },
  {
    path: 'client/:id',
    component: ClientViewComponent
  },
  {
    path: 'client/:id/edit',
    component: ClientFormComponent,
    //tu będzie dodawane zabezpieczenie wejścia na stronę
  },
  {
    path: 'appusers',
    component: AppUserListComponent
  },
  {
    path: 'appusers/add',
    component: AddAppUserComponent
  },
  {
    path: 'appusers/edit/:id',
    component: EditAppUserComponent
  },
  {
    path: 'note',
    component: NoteComponent,
    children: [

      { path: '', component: NoteIndexComponent },
      { path: 'add', component: NoteFormComponent },
      { path: 'edit/:id', component: NoteFormComponent}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
