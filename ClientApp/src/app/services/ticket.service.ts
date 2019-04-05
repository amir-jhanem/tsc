import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';

// import 'rxjs/add/operator/map';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private readonly ticketsEndpoint = '/api/tickets';

  constructor(private http: Http) { }

  
  create(ticket) {
    return this.http.post(this.ticketsEndpoint, ticket)
      .pipe(map(res => res.json()));
  }
}
