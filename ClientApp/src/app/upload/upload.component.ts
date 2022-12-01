import { Component, OnInit } from "@angular/core";
import { UploadService } from "../services/upload.service";

@Component({
  selector: "app-upload",
  templateUrl: "./upload.component.html",
  styleUrls: ["./upload.component.css"],
})
export class UploadComponent implements OnInit {
  public progress: number;
  public message: string;
  baseUrl: string;

  constructor(private uploadService: UploadService) {}

  ngOnInit() {}

  public uploadFile = () => {
    const files = (
      document.querySelector("input[type=file]") as HTMLInputElement
    ).files;

    if (files.length === 0) {
      return;
    }

    const fileToUpload = files[0];

    this.uploadService.uploadFile(fileToUpload);

    //this.http.post(this.baseUrl + 'file', formData, { reportProgress: true, observe: 'events' })

    //  .subscribe(event => {
    //    if (event.type === HttpEventType.UploadProgress)
    //      this.progress = Math.round(100 * event.loaded / event.total);
    //    else if (event.type === HttpEventType.Response) {
    //      this.message = 'Upload success.';
    //      this.onUploadFinished.emit(event.body);
    //    }
    //  });
  };
}
