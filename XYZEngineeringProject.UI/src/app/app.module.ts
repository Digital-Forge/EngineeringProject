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
import { ClientFormComponent } from './components/client/clientForm/client-form.component';
import { ClientViewComponent } from './components/client/clientView/client-view.component';
import { ClientComponent } from './components/client/clientIndex/client-index.component';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {TranslateHttpLoader} from '@ngx-translate/http-loader';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { ClientFiltersComponent } from './components/client/clientFilters/filters.component';
import { NoteFilterComponent } from './components/note/note-filter/note-filter.component';
import { NoteIndexComponent } from './components/note/note-index/note-index.component';
import { NoteFormComponent } from './components/note/note-form/note-form.component';
import { CalendarComponent } from './components/calendar/calendar/calendar.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { TaskListComponent } from './components/task-list/index/task-list-index.component';
import { TaskListFormComponent } from './components/task-list/form/task-list-form.component';
import { TaskListFiltersComponent } from './components/task-list/filters/task-list-filters.component';
// import { TaskComponent } from './components/task-list/task/index/task-index.component';
import { TaskFormComponent } from './components/task-list/task/form/task-form.component';
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
import { DateCustom } from './shared/pipes/dateLong.pipe';
import { DateShort } from './shared/pipes/dateShort.pipe';

@NgModule({
  declarations: [
    AppComponent,
    AuthorizationComponent,
    LoginComponent,
    ClientFormComponent,
    ClientViewComponent,
    ClientComponent,
    ClientFiltersComponent,
    NavbarComponent,
    NoteFilterComponent,
    NoteIndexComponent,
    NoteFormComponent,
    CalendarComponent,
    DashboardComponent,
    TaskListComponent,
    TaskListFormComponent,
    TaskListFiltersComponent,
    // TaskComponent,
    TaskFormComponent,
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
    DateShort
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