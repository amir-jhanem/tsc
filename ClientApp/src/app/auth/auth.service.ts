import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  profile: any;
  private roles: string[] = []; 
  constructor() { }

  public authenticated() {

    if (localStorage.getItem('userToken') != null)
      return true;

    return false;
  }
  public logout() {
    // Remove token from localStorage
    localStorage.removeItem('userToken');
    localStorage.removeItem('profile');
    this.profile = null;
    this.roles = [];
  }
}
