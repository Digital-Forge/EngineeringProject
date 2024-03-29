import { ChangePasswordComponent } from './components/settings/change-password/change-password.component';
import { RegisterComponent } from './components/register/register.component';
import { DepartmentIndexComponent } from './components/settings/department/department-index/department-index.component';
import { ForumComponent } from './components/forum/forum.component';
import { UserViewComponent } from './components/settings/user/user-view/user-view.component';
import { DocumentComponent } from './components/document/document.component';
import { UserFormComponent } from './components/settings/user/user-form/user-form.component';
import { SettingsComponent } from './components/settings/settings.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { ClientComponent } from './components/client/clientIndex/client-index.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { LoginComponent } from './components/login/login.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { AuthGuard } from './services/auth.guard';
import { ClientFormNewComponent } from './components/client/client-form-new/client-form-new.component';
import { NoteFormNewComponent } from './components/note/note-form-new/note-form-new.component';
import { NoteViewComponent } from './components/note/note-view/note-view.component';
import { TaskListFormNewComponent } from './components/task-list/task-list-form-new/task-list-form-new.component';
import { TaskListViewComponent } from './components/task-list/view/task-list-view.component';
import { TaskFormNewComponent } from './components/task-list/task/form-new/task-form-new.component';
import { UserIndexComponent } from './components/settings/user/user-index/user-index.component';
import { DepartmentViewComponent } from './components/settings/department/department-view/department-view.component';
import { DepartmentFormComponent } from './components/settings/department/department-form/department-form.component';

const routes: Routes = [

  {
    path: '',
    component: LoginComponent
  }, 
  {
    path: 'register',
    component: RegisterComponent
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    canActivate: [AuthGuard]
  },
  {
    path: 'task-list',
    children: [
      { path: '', component: TaskListComponent },
      { path: 'add', component: TaskListFormNewComponent },
      { path: ':id', component: TaskListViewComponent },
      { path: 'edit/:id', component: TaskListFormNewComponent },
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'task',
    children: [
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
    path: 'forum',
    component: ForumComponent,
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
      },
      {
        path: 'departments',
        children: [
          { path: 'add', component: DepartmentFormComponent },
          { path: 'edit/:id', component: DepartmentFormComponent },
          { path: ':id', component: DepartmentViewComponent },
          { path: '', component: DepartmentIndexComponent }
        ]
      },
      {
        path: 'changePassword',
        component: ChangePasswordComponent
      }
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'documents',
    children: [
      {path:'', component: DocumentComponent},
    ],
    canActivate: [AuthGuard]
  },
  {
    path: 'login',
    component: LoginComponent
  },
  {
    path: '**',
    redirectTo: 'login'
  },
 
 
 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
