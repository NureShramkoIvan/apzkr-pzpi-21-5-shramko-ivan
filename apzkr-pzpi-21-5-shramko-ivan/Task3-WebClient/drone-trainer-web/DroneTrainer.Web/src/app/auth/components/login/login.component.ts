import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { jwtDecode } from 'jwt-decode';
import { AuthApiService } from '../../../core/services/auth-api.service';
import { AccessTokenVM } from '../../../core/models/auth/access-token-vm.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private authService: AuthApiService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const token = localStorage.getItem('accessToken');

    if (!!token) {
      this.router.navigate(['home']);
      return;
    }

    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    const username = this.loginForm.get('username')?.value;
    const password = this.loginForm.get('password')?.value;

    this.authService.signIn({ userName: username, password }).subscribe(
      (token: AccessTokenVM) => {
        localStorage.setItem('accessToken', token.accessToken);
        const decodedToken: any = jwtDecode(token.accessToken);
        localStorage.setItem('role', decodedToken.role_id);

        this.router.navigate(['home']);
      },
      (error) => {
        console.error('Login error:', error);
      }
    );
  }
}
