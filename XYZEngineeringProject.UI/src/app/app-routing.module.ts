import { ClientGroupFormComponent } from './components/client/client-group-form/client-group-form.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CalendarComponent } from './components/calendar/calendar/calendar.component';
import { NoteFormComponent } from './components/note/note-form/note-form.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { NoteComponent } from './components/note/note.component';
import { TaskFormComponent } from './components/task-list/task/form/task-form.component';
import { ClientIndexComponent } from './components/client/clientIndex/client-index.component';
import { ClientFormComponent } from './components/client/clientForm/client-form.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { ClientComponent } from './components/client/client.component';
import { EditAppUserComponent } from './components/appUser/edit-app-user/edit-app-user.component';
import { AddAppUserComponent } from './components/appUser/add-app-user/add-app-user.component';
import { AppUserListComponent } from './components/appUser/app-user-list/app-user-list.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskComponent } from './components/task-list/task/index/task-index.component';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { AuthGuard } from './services/auth.guard';
import { TaskListFormComponent } from './components/task-list/form/task-list-form.component';
import { HomePageComponent } from './components/home-page/home-page.component';

const routes: Routes = [
  {
    path: '',
    component: HomePageComponent
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard] 
  },
  {
    path: 'task-list', //TODO trzymajmy się liczby pojedynczej może bo będziemy mieć widok listy pod /task i widok konkretnego taska pod task/123
    children: [
      { path: '', component: TaskListComponent },
      { path: 'add', component: TaskListFormComponent },
      { path: ':id', component: TaskComponent },
      { path: 'edit/:id', component: TaskListFormComponent },
    ],
    canActivate: [AuthGuard] 
  },
  {
    path: 'task',
    children: [
     // { path: ':id', component: TaskComponent},
      { path: 'add', component: TaskFormComponent},
      { path: 'edit/:id', component: TaskFormComponent},
    ],
    canActivate: [AuthGuard] 
  },
  {
    path: 'clients',
    component: ClientComponent,
    children: [
      {path: '', component: ClientIndexComponent},
      {path: 'add', component: ClientFormComponent},
      {path: 'edit/:id', component: ClientFormComponent},
      {path: 'group/add', component: ClientGroupFormComponent},
      {path: 'group/edit/:id', component: ClientGroupFormComponent}
    ],
    canActivate: [AuthGuard] 
  },
  {
    path: 'appusers',
    component: AppUserListComponent,
    canActivate: [AuthGuard] // TODO zabezpieczenie dostępu tylko dla wybranych ról ?
  },
  {
    path: 'appusers/add',
    component: AddAppUserComponent,
    canActivate: [AuthGuard] // TODO zabezpieczenie dostępu tylko dla wybranych ról ?
  },
  {
    path: 'appusers/edit/:id',
    component: EditAppUserComponent,
    canActivate: [AuthGuard] // TODO zabezpieczenie dostępu tylko dla wybranych ról ?
  },
  {
    path: 'note',
    component: NoteComponent,
    children: [
      { path: '', component: NoteIndexComponent },
      { path: 'add', component: NoteFormComponent },
      { path: 'edit/:id', component: NoteFormComponent}
    ],
    canActivate: [AuthGuard] 
  },
  {
    path: 'calendar',
    component: CalendarComponent,
    children: [
      { path: '', component: CalendarComponent },
      // { path: 'add', component: UserTaskFormComponent },
      // { path: 'edit/:id', component: UserTaskFormComponent },
    ],
    canActivate: [AuthGuard] 
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
