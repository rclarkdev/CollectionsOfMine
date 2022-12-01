import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};
@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) { }

  login(model: any): Observable<any> {
    return this.http.post(environment.apiRoot + 'users/' + 'Authenticate', model, httpOptions);
  }

  register(model: any): Observable<any> {

    return this.http.post(environment.apiRoot + 'users/' + 'CreateUser', model, httpOptions);
  }
}
