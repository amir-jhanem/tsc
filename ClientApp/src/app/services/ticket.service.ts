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

  delete(id) {
    return this.http.delete(this.ticketsEndpoint+ '/' + id)
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
