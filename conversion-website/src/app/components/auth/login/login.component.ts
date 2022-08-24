import Swal from 'sweetalert2';
import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { LoginRequest } from 'src/app/shared/models/login-request.model';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginRequest: LoginRequest = {};
  constructor(private authService: AuthService,  private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {
      this.authService.user$.subscribe(u => {
        if (u){
          this.router.navigate(['./home'], { relativeTo: this.route });
        }
      });
  }

  saveChanges(form: NgForm): void {
      Swal.showLoading();
      this.authService.signIn(form.value).subscribe(() => {
         Swal.close();
         this.router.navigate(['./home'], { relativeTo: this.route });
      });
  }
}