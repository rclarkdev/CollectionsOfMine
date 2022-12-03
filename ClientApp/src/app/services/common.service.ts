import { Injectable } from "@angular/core";

import { HttpClient } from "@angular/common/http";
import { BehaviorSubject } from "rxjs";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: "root",
})
export class CommonService<Type> {
  private apiUrl: string;

  private base64ImgSource = new BehaviorSubject<string>("");
  base64ImgObservable = this.base64ImgSource.asObservable();

  private typesSource = new BehaviorSubject<Type[]>([]);
  typesObservable = this.typesSource.asObservable();

  private typeSource = new BehaviorSubject<any>({});
  typeObservable = this.typeSource.asObservable();

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiRoot;
  }

  public create = async (type, route) => {
    return await this.http.post(this.apiUrl + route, type).toPromise();
  };

  public update = async (type, route) => {
    return await this.http.patch(this.apiUrl + route, type).toPromise();
  };

  public delete = async (route) => {
    return await this.http.delete(this.apiUrl + route).toPromise();
  };

  getAll = async(route) => {
    return await this.http.get<Type[]>(this.apiUrl + route).toPromise();
  };

  getById = async (id) => {
    return await this.http
      .get<Type>(this.apiUrl + "/GetById/" + id)
      .toPromise();
  };

  getByName = async (name) => {
    return await this.http
      .get<Type>(this.apiUrl + "/GetByName/" + name)
      .toPromise();
  };

  setType = async (type) => {
    this.typeSource.next(type);
  };

  setTypes = async (types) => {
    this.typesSource.next(types);
  };
}
