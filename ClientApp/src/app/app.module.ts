import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';

import { TicketService } from './services/ticket.service';
import { NavMenuComponent } from './components/shared/nav-menu/nav-menu.component';
import { HomeComponent } from './components/home/home.component';

import { TicketFormComponent } from './components/tickets/ticket-form/ticket-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { TicketListComponent } from './components/tickets/ticket-list/ticket-list.component';
import { TicketViewComponent } from './components/tickets/ticket-view/ticket-view.component';
import { PaginationComponent } from './components/shared/pagination/pagination.component';
import { GroupListComponent } from './components/groups/group-list/group-list.component';
import { GroupFormComponent } from './components/groups/group-form/group-form.component';
import { SignUpComponent } from './components/user/sign-up/sign-up.component';
import { SignInComponent } from './components/user/sign-in/sign-in.component';
import { AuthGuard } from './auth/auth.guard';
import { AuthInterceptor } from './auth/auth.interceptor';
import { UserService } from './services/user/user.service';

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    SignInComponent,
    NavMenuComponent,
    HomeComponent,
    TicketFormComponent,
    TicketListComponent,
    TicketViewComponent,
    PaginationComponent,
    GroupListComponent,
    GroupFormComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpModule,
    HttpClientModule,
    FormsModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot([
      { path: '', redirectTo: 'tickets', pathMatch: 'full' },
      { path: 'register', component: SignUpComponent,canActivate:[AuthGuard] },
      { path: 'login', component: SignInComponent },
      { path: 'tickets/new', component: TicketFormComponent },
      { path: 'tickets/:id', component: TicketViewComponent,canActivate:[AuthGuard] },
      { path: 'tickets', component: TicketListComponent,canActivate:[AuthGuard] },
      { path: 'groups/:id', component: GroupFormComponent,canActivate:[AuthGuard] },
      { path: 'groups', component: GroupListComponent,canActivate:[AuthGuard] },
      { path: 'home', component: HomeComponent },
      { path: '**', redirectTo: 'home' },


    ])
  ],
  providers: [TicketService,UserService,
    AuthGuard,
    {
      provide : HTTP_INTERCEPTORS,
      useClass : AuthInterceptor,
      multi : true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
