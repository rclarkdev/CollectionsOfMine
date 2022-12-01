import { Component, OnInit } from "@angular/core";

@Component({
  selector: "app-tinymce",
  templateUrl: "./tinymce.component.html",
  styleUrls: ["./tinymce.component.css"],
})
export class TinymceComponent implements OnInit {
  constructor() {}

  filePickerCallback = function (callback, value, meta) {
    var input = document.createElement("input");
    input.setAttribute("type", "file");
    input.setAttribute("accept", "image/*");

    input.click();

    input.onchange = function () {
      var file = input.files[0];
      var reader = new FileReader();
      reader.onload = function (e) {
        callback(e.target.result, {
          alt: file.name,
        });
      };
      reader.readAsDataURL(file);
    };
  };

  ngOnInit(): void {}
}
