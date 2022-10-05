import { AuthorizationService } from './services/authorization/authorization.service';
import { NavbarComponent } from './shared/navbar/navbar/navbar.component';
import { InterceptorService } from './services/interceptor/interceptor.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { ClientComponent } from './components/client/client.component';
import { ClientFormComponent } from './components/client/clientForm/client-form.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { ClientIndexComponent } from './components/client/clientIndex/client-index.component';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { AppUserListComponent } from './components/appUser/app-user-list/app-user-list.component';
import { AddAppUserComponent } from './components/appUser/add-app-user/add-app-user.component';
import { EditAppUserComponent } from './components/appUser/edit-app-user/edit-app-user.component';
import { ClientFiltersComponent } from './components/client/clientFilters/filters.component';

import { NoteComponent } from './components/note/note.component';
import { NoteFilterComponent } from './components/note/note-filter/note-filter.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { NoteFormComponent } from './components/note/note-form/note-form.component';
import { CalendarComponent } from './components/calendar/calendar/calendar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { TaskListFormComponent } from './components/task-list/form/task-list-form.component';
import { TaskListFiltersComponent } from './components/task-list/filters/task-list-filters.component';
import { TaskComponent } from './components/task-list/task/index/task-index.component';
import { TaskFormComponent } from './components/task-list/task/form/task-form.component';
import { TaskFiltersComponent } from './components/task-list/task/filters/task-filters.component';
import { ClientGroupFormComponent } from './components/client/client-group-form/client-group-form.component';

@NgModule({
  declarations: [
    AppComponent,
    AuthorizationComponent,
    LoginComponent,
    ClientComponent,
    ClientFormComponent,
    ClientViewComponent,
    ClientIndexComponent,
    ClientFiltersComponent,
    AppUserListComponent,
    AddAppUserComponent,
    EditAppUserComponent,
    NavbarComponent,
    NoteComponent,
    NoteFilterComponent,
    NoteIndexComponent,
    NoteFormComponent,
    CalendarComponent,
    DashboardComponent,
    TaskListComponent,
    TaskListFormComponent,
    TaskListFiltersComponent,
    TaskComponent,
    TaskFormComponent,
    TaskFiltersComponent,
    ClientGroupFormComponent
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
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass:InterceptorService, multi: true},
    AuthorizationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function HttpLoaderFactory(http: HttpClient): TranslateHttpLoader {
  return new TranslateHttpLoader(http);
}