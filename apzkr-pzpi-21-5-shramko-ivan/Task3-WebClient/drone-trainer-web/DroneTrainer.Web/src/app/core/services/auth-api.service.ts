import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenGetVM } from '../models/auth/token-get-vm.model';
import { AccessTokenVM } from '../models/auth/access-token-vm.model';
import { API_URL } from '../../shared/constants';

@Injectable({
  providedIn: 'root',
})
export class AuthApiService {
  private readonly apiUrl = API_URL;

  constructor(private http: HttpClient) {}

  signIn(tokenGetVM: TokenGetVM): Observable<AccessTokenVM> {
    return this.http.post<AccessTokenVM>(`${this.apiUrl}/token/`, tokenGetVM);
  }
}
