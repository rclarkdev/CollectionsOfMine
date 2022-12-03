import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  id = 0;
  isSuccessful = false;
  isSignUpFailed = false;
  errorMessage = '';
  email = '';
  password = '';
  username = '';
  lastname = '';
  firstname = '';

  constructor(private authService: AuthService) { }
  ngOnInit(): void {
  }
  onSubmit(data): void {

    this.authService.register(data.value).subscribe({
      next: data => {
        console.log(data);
        this.isSuccessful = true;
        this.isSignUpFailed = false;
      },
      error: err => {
        this.errorMessage = err.message;
        this.isSignUpFailed = true;
      }
    });
  }
}
