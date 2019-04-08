import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr'
import { UserService } from 'src/app/services/user/user.service';
import { User } from 'src/app/services/user/user.model';
import { Router } from '@angular/router';


@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: User;
  emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$";

  constructor(private userService: UserService,private router: Router, private toastrService: ToastrService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if (form != null)
      form.reset();
    this.user = {
      UserName: '',
      Password: '',
      Email: '',
      FullName: '',
      IsAdmin: false
    }
  }

  OnSubmit(form: NgForm) {
    console.log(form.value);
    this.userService.registerUser(form.value)
    .subscribe(t=>{
      console.log(t);
      this.toastrService.success('User Created Success', 'Success!');
      this.router.navigate(['/home']);
    },
    err =>{
      this.toastrService.error('An unexpected error happened', 'Error', {
        timeOut: 3000
      });

    });
  }

}
