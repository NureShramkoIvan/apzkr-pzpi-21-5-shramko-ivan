import { Component, OnInit } from '@angular/core';
import { UserApiService } from '../../../core/services/user-api.service';
import { UserVM } from '../../../core/models/auth/user-vm.model';
import { UserCreateUpdateModalComponent } from '../user-create-update-modal/user-create-update-modal.component';
import { MatDialog } from '@angular/material/dialog';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  users: UserVM[] = [];

  constructor(
    private userApiService: UserApiService,
    private dialog: MatDialog,
    private translate: TranslateService
  ) {}

  ngOnInit(): void {
    this.loadUsers();
    const locale = localStorage.getItem('locale') || 'uk-UA';
    this.translate.use(locale);
  }

  loadUsers() {
    this.userApiService.getUsers().subscribe(
      (users: UserVM[]) => {
        this.users = users;
      },
      (error) => {
        console.error('Error fetching users:', error);
      }
    );
  }

  openCreateUpdateModal(): void {
    const dialogRef = this.dialog.open(UserCreateUpdateModalComponent, {
      width: '400px',
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.loadUsers();
    });
  }
}
