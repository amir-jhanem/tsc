import { AuthService } from 'src/app/auth/auth.service';
import { Injectable, Inject } from '@angular/core';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { Http, Headers, Response } from '@angular/http';


@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private readonly ticketsEndpoint = '/api/tickets';

  constructor(private http: Http,private auth :AuthService) { }

  get(id) {
    return this.http.get(this.ticketsEndpoint+ '/' + id)
      .pipe(map(res => res.json()));
  }

  getAll(filter) {
    return this.http.get(this.ticketsEndpoint + '?' + this.toQueryString(filter))
      .pipe(map(res => res.json()));
  }

  create(ticket) {
    return this.http.post(this.ticketsEndpoint, ticket)
      .pipe(map(res => res.json()));
  }

  delete(id) : Observable<any>  {
    const headers = new Headers({
      'Content-Type': 'application/json',
      'Authorization': this.auth.getToken()
    });
    console.log(this.auth.getToken());
    return this.http.delete(this.ticketsEndpoint+ '/' + id, { headers: headers })
      .pipe(map(res => res.json()));
  }

  assignTicket(ticketId,groupId) {
    var data = {ticketId : ticketId ,groupId:groupId};
    console.log(data);

    return this.http.post(this.ticketsEndpoint+"/AssinTickets",data )
      .pipe(map(res => res.json()));
  }

  toQueryString(obj) {
    var parts = [];
    for (var property in obj) {
      var value = obj[property];
      if (value != null && value != undefined) 
        parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
    }

    return parts.join('&');
  }


}
