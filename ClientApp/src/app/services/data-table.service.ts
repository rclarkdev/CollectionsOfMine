import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class DataTableService {
  constructor(private http: HttpClient) {}

  dtTrigger: Subject<any> = new Subject<any>();

  private dtOptionsBS = new BehaviorSubject<any>(null);
  dtOptions = this.dtOptionsBS.asObservable();

  setDtOptions(data) {
    this.dtOptionsBS.next(data);
  }

  setDtTrigger(data) {
    this.dtTrigger.next(data);
  }
}
