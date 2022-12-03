import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { ICollection } from "../interfaces/collection.interface";
import { IItem } from "../interfaces/item.interface";
import { environment } from "../../environments/environment";
import { Route } from "@angular/router";

@Injectable({
  providedIn: "root",
})
export class ModelListService {
  private apiUrl: string;

  constructor(private http: HttpClient) {
    this.apiUrl = environment.apiRoot;
  }

  getCollectionsByAreaId = async (areaId: string) => {
    return await this.http
      .get<ICollection[]>(this.apiUrl + "/AreaCollections/" + areaId)
      .toPromise();
  };

  getItemsByCollectionId = async (collectionId: string) => {
    return await this.http
      .get<IItem[]>(this.apiUrl + "/CollectionItems/" + collectionId)
      .toPromise();
  };
}
