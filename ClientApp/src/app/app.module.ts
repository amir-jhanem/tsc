import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { HttpClientModule } from '@angular/common/http';
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

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    TicketFormComponent,
    TicketListComponent,
    TicketViewComponent,
    PaginationComponent
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
      { path: 'tickets/new', component: TicketFormComponent },
      { path: 'tickets/:id', component: TicketViewComponent },
      { path: 'tickets', component: TicketListComponent },
      { path: 'home', component: HomeComponent },
      { path: '**', redirectTo: 'home' },


    ])
  ],
  providers: [TicketService],
  bootstrap: [AppComponent]
})
export class AppModule { }
