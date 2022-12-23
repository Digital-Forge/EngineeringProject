import { UserViewComponent } from './components/settings/user/user-view/user-view.component';
import { DocumentComponent } from './components/document/document.component';
import { UserFormComponent } from './components/settings/user/user-form/user-form.component';
import { UserComponent } from './components/settings/user/user.component';
import { SettingsComponent } from './components/settings/settings.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CalendarComponent } from './components/calendar/calendar/calendar.component';
import { NoteFormComponent } from './components/note/note-form/note-form.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { TaskFormComponent } from './components/task-list/task/form/task-form.component';
import { ClientComponent } from './components/client/clientIndex/client-index.component';
import { ClientFormComponent } from './components/client/clientForm/client-form.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
// import { TaskComponent } from './components/task-list/task/index/task-index.component';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { AuthGuard } from './services/auth.guard';
import { TaskListFormComponent } from './components/task-list/form/task-list-form.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ClientFormNewComponent } from './components/client/client-form-new/client-form-new.component';
import { NoteFormNewComponent } from './components/note/note-form-new/note-form-new.component';
import { NoteViewComponent } from './components/note/note-view/note-view.component';
import { TaskListFormNewComponent } from './components/task-list/task-list-form-new/task-list-form-new.component';
import { TaskListViewComponent } from './components/task-list/view/task-list-view.component';
import { TaskFormNewComponent } from './components/task-list/task/form-new/task-form-new.component';
import { UserIndexComponent } from './components/settings/user/user-index/user-index.component';

const routes: Routes = [

  {
    path: 'task-list', //TODO trzymajmy się liczby pojedynczej może bo będziemy mieć widok listy pod /task i widok konkretnego taska pod task/123
    children: [
      { path: '', component: TaskListComponent },
      // { path: 'add', component: TaskListFormComponent },
      { path: 'add', component: TaskListFormNewComponent },
      { path: ':id', component: TaskListViewComponent },
      // { path: ':id', component: TaskComponent },
      // { path: 'edit/:id', component: TaskListFormComponent },
      { path: 'edit/:id', component: TaskListFormNewComponent },
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'task',
    children: [
      // { path: ':id', component: TaskComponent},
      // { path: 'add', component: TaskFormComponent},
      { path: 'add', component: TaskFormNewComponent },
      { path: 'add/:listId', component: TaskFormNewComponent },
      { path: 'edit/:taskId', component: TaskFormNewComponent },
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'clients',
    children: [
      { path: '', component: ClientComponent },
      { path: 'add', component: ClientFormNewComponent },
      { path: ':id', component: ClientViewComponent },
      { path: 'edit/:id', component: ClientFormNewComponent }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'note',
    children: [
      { path: '', component: NoteIndexComponent },
      { path: 'add', component: NoteFormNewComponent },
      { path: ':id', component: NoteViewComponent },
      { path: 'edit/:id', component: NoteFormNewComponent }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'calendar',
    children: [
      { path: '', component: CalendarComponent },
      // { path: 'add', component: UserTaskFormComponent },
      // { path: 'edit/:id', component: UserTaskFormComponent },
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'settings',
    children: [
      { path: '', component: SettingsComponent },
      {
        path: 'users',
        children: [
          { path: 'add', component: UserFormComponent },
          { path: 'edit/:id', component: UserFormComponent },
          { path: ':id', component: UserViewComponent },
          { path: '', component: UserIndexComponent }
        ]
      }
    ]
  },
  {
    path: 'documents',
    children: [
      {path:'', component:DocumentComponent},
    ]
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
    path: '',
    component: HomePageComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
