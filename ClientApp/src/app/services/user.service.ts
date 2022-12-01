import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from "@angular/common/http";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  private _http: HttpContext;

  public UserService(http: HttpContext)
  {
    this._http = http;
  }

  private userSubject = new BehaviorSubject<any>(null);
  userObservable = this.userSubject.asObservable();

  setUser(data) {
    this.userSubject.next(data);
  }

  //getPublicContent(): Observable<any> {
  //  return this._http.get(API_URL + 'all', { responseType: 'text' });
  //}
  //getUserBoard(): Observable<any> {
  //  return this.http.get(API_URL + 'user', { responseType: 'text' });
  //}
  //getModeratorBoard(): Observable<any> {
  //  return this.http.get(API_URL + 'mod', { responseType: 'text' });
  //}
  //getAdminBoard(): Observable<any> {
  //  return this.http.get(API_URL + 'admin', { responseType: 'text' });
  //}
}
