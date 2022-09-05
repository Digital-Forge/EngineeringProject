import { ClientIndexComponent } from './components/client/index/client-index.component';
import { ClientFormComponent } from './components/client/form/client-form.component';
import { ClientViewComponent } from './components/client/view/client-view.component';
import { ClientComponent } from './components/client/client.component';
import { LoginComponent } from './components/login/login.component';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { EditTaskComponent } from './components/tasks/edit-task/edit-task.component';
import { AddTaskComponent } from './components/tasks/add-task/add-task.component';
import { TaskListComponent } from './components/tasks/task-list/task-list.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    component: LoginComponent
  },
  {
    path: 'tasks',
    component: TaskListComponent
  },
  {
    path: 'tasks/add',
    component: AddTaskComponent
  },
  {
    path: 'tasks/edit/:id',
    component: EditTaskComponent
  },
  {
    path: 'client',
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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
