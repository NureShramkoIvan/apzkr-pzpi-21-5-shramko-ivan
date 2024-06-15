import { Component, OnInit } from '@angular/core';
import { Role } from '../../../shared/enums/role.enum';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css'],
})
export class HeaderComponent implements OnInit {
  isLoggedIn: boolean = false;
  isAdmin: boolean = false;
  isInstructorOrAdmin: boolean = false;
  locale: string = 'uk-UA';

  constructor(private translate: TranslateService) {}

  ngOnInit(): void {
    this.checkAuthentication();
    this.locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(this.locale);
    this.translate.onLangChange.emit();
  }

  checkAuthentication(): void {
    const token = localStorage.getItem('accessToken');
    const role = localStorage.getItem('role');

    this.isLoggedIn = !!token;
    this.isAdmin = Number.parseInt(role || '') == <Number>Role.Admin;
    this.isInstructorOrAdmin =
      Number.parseInt(role || '') == <Number>Role.Instructor || this.isAdmin;
  }

  onLocaleChange(event: any) {
    console.log(this.locale);

    localStorage.setItem('locale', this.locale);

    this.translate.use(this.locale);
    this.translate.onLangChange.emit();
    window.location.reload();
  }
}
