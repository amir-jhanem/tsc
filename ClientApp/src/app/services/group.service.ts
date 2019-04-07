import { Injectable, Inject } from '@angular/core';
import { Http } from '@angular/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private readonly groupsEndpoint = '/api/groups';

  constructor(private http: Http) { }

  get(id) {
    return this.http.get(this.groupsEndpoint+ '/' + id)
      .pipe(map(res => res.json()));
  }

  getAll(filter) {
    return this.http.get(this.groupsEndpoint + '?' + this.toQueryString(filter))
      .pipe(map(res => res.json()));
  }

  getMembers() {
    return this.http.get("/api/AuthController/GetMembers")
      .pipe(map(res => res.json()));
  }

  create(group) {
    return this.http.post(this.groupsEndpoint, group)
      .pipe(map(res => res.json()));
  }
  update(group) {
    return this.http.put(this.groupsEndpoint, group)
      .pipe(map(res => res.json()));
  }

  delete(id) {
    return this.http.delete(this.groupsEndpoint+ '/' + id)
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