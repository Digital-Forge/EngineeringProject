import { DepartmentViewComponent } from './components/settings/department/department-view/department-view.component';
import { AuthorizationService } from './services/authorization/authorization.service';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { InterceptorService } from './services/interceptor/interceptor.service';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HttpClient, HTTP_INTERCEPTORS } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './components/login/login.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { ClientComponent } from './components/client/clientIndex/client-index.component';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { NoteFilterComponent } from './components/note/note-filter/note-filter.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { TaskListFiltersComponent } from './components/task-list/filters/task-list-filters.component';
import { TaskFiltersComponent } from './components/task-list/task/filters/task-filters.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { ClientFormNewComponent } from './components/client/client-form-new/client-form-new.component';
import { NoteFormNewComponent } from './components/note/note-form-new/note-form-new.component';
import { NoteViewComponent } from './components/note/note-view/note-view.component';
import { TaskListFormNewComponent } from './components/task-list/task-list-form-new/task-list-form-new.component';
import { TaskListViewComponent } from './components/task-list/view/task-list-view.component';
import { TaskFormNewComponent } from './components/task-list/task/form-new/task-form-new.component';
import { SettingsComponent } from './components/settings/settings.component';
import { UserComponent } from './components/settings/user/user.component';
import { UserFormComponent } from './components/settings/user/user-form/user-form.component';
import { UserViewComponent } from './components/settings/user/user-view/user-view.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DocumentComponent } from './components/document/document.component';
import { DateCustom } from './shared/pipes/dateCustom.pipe';
import { UserIndexComponent } from './components/settings/user/user-index/user-index.component';
import { RegisterComponent } from './components/register/register.component';
import { ForumComponent } from './components/forum/forum.component';
import { DepartmentIndexComponent } from './components/settings/department/department-index/department-index.component';
import { DepartmentFormComponent } from './components/settings/department/department-form/department-form.component';
import { ChangePasswordComponent } from './components/settings/change-password/change-password.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ClientViewComponent,
    ClientComponent,
    NavbarComponent,
    NoteFilterComponent,
    NoteIndexComponent,
    DashboardComponent,
    TaskListComponent,
    TaskListFiltersComponent,
    TaskFiltersComponent,
    HomePageComponent,
    TaskFiltersComponent,
    ClientFormNewComponent,
    NoteFormNewComponent,
    NoteViewComponent,
    TaskListFormNewComponent,
    TaskListViewComponent,
    TaskFormNewComponent,
    SettingsComponent,
    UserComponent,
    UserFormComponent,
    UserViewComponent,
    DocumentComponent,
    DateCustom,
    UserIndexComponent,
    RegisterComponent,
    ForumComponent,
    UserIndexComponent,
    DepartmentIndexComponent,
    DepartmentFormComponent,
    DepartmentViewComponent,
    ChangePasswordComponent
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
    }),
    BrowserAnimationsModule,
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