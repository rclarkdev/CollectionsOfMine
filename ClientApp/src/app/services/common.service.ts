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

  private typeSource = new BehaviorSubject<Type>(null);
  typeObservable = this.typeSource.asObservable();

  constructor(private route: string, private http: HttpClient = null) {
    this.apiUrl = environment.apiRoot + route;
  }

  public create = async (type) => {
    return await this.http.post(this.apiUrl, type).toPromise();
  };

  public update = async (type) => {
    return await this.http.patch(this.apiUrl, type).toPromise();
  };

  public delete = async (id) => {
    return await this.http.delete(this.apiUrl + "/" + id).toPromise();
  };

  getAll = async () => {
    return await this.http.get<Type[]>(this.apiUrl).toPromise();
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
