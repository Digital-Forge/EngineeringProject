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
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
