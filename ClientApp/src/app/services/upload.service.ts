import {
  Injectable
} from "@angular/core";

import { HttpEventType, HttpClient } from "@angular/common/http";
import { environment } from "../../environments/environment";

@Injectable({
  providedIn: "root",
})
export class UploadService {

  constructor(private http: HttpClient) {
  }

  public uploadFile = async (fileToUpload) => {
    const formData = new FormData();
    formData.append("file", fileToUpload, fileToUpload.name);

    return await this.http
      .post(environment.apiRoot + "files", formData, {
        reportProgress: true,
        observe: "events",
      })
      .toPromise();
  };
}
