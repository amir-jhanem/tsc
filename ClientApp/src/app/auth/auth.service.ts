import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private router: Router) { }

  public authenticated(isAdmin) {
    var userToken =  JSON.parse(localStorage.getItem('userToken'));
    console.log(userToken);
    if (userToken != null)
    {
      if(isAdmin)
      {
        if(userToken.isAdmin)
        {
          return true;
        }
        else
        {
          return false;
        }
      }
      return true;
    }
    return false;
  }

  public getCurrentName(){
    var userToken = localStorage.getItem('userToken');

    return JSON.parse(userToken).fullName;
  }
  public logout() {
    // Remove token from localStorage
    localStorage.removeItem('userToken');
    this.router.navigate(['/home']);
  }
}
