import { Injectable } from '@angular/core';
import { Http } from '@angular/http';

import { User } from './user.model';
import { map } from 'rxjs/operators';

@Injectable()
export class UserService {
  private readonly authEndpoint = '/api/AuthController';

  constructor(private http: Http) { }

  registerUser(user: User) {
    const body: User = {
      UserName: user.UserName,
      Password: user.Password,
      Email: user.Email,
      FullName: user.FullName,
      IsAdmin: user.IsAdmin
    }
    return this.http.post(this.authEndpoint + '/Register', body)
      .pipe(map(res => res.json()));
  }

  userAuthentication(userName, password) {
    var data = {UserName:userName, Password:password};
    console.log(data);
    return this.http.post("/api/AuthController/Login", data)
    .pipe(map(res => res.json()));

    ;
  }

  getUserClaims(){
   return  this.http.get(this.authEndpoint+'/api/GetUserClaims');
  }

}
