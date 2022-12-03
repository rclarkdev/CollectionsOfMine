import { AfterViewInit, Component, Input, OnInit } from "@angular/core";
import { ICollection } from "../interfaces/collection.interface";
import { IItem } from "../interfaces/item.interface";
import { IArea } from "../interfaces/area.interface";
import { IFile } from "../interfaces/file.interface";
import { IAttribute } from "../interfaces/attribute.interface";
import { HttpClient } from "@angular/common/http";
import { CommonService } from "../services/common.service";
import { UploadService } from "../services/upload.service";
import { ActivatedRoute, ParamMap } from "@angular/router";
import { ModelListService } from "../services/model-list.service";
import { DomSanitizer, SafeHtml } from '@angular/platform-browser';

declare var tinymce: any;

@Component({
  selector: "app-item",
  templateUrl: "./item.component.html",
  styleUrls: ["./item.component.css"],
})
export class ItemComponent implements OnInit {

  sanitizedText: SafeHtml = '';
  item: IItem;
  collectionViewModel: ICollection;
  attributeViewModels: IAttribute[];
  selectedAttributes: IAttribute[];
  areaViewModels: IArea[];
  itemService: CommonService<IItem>;
  areaService: CommonService<IArea>;
  collectionService: CommonService<ICollection>;
  attributeService: CommonService<IAttribute>;
  modelListService: ModelListService;
  showCollections: boolean;
  isCreateOrEdit: boolean;
  collectionId: string;
  files: FileList;
  sanatizedHtml: SafeHtml = '';

  @Input() createOrEditItem: boolean;
  @Input() areaId: string;
  @Input() itemId: string;
  @Input() collection: ICollection;

  constructor(
    private uploadService: UploadService,
    private http: HttpClient,
    private route: ActivatedRoute,
    private sanitizer: DomSanitizer
  ) {
    this.itemService = new CommonService(http);
    this.areaService = new CommonService(http);
    this.collectionService = new CommonService(http);
    this.attributeService = new CommonService(http);
  }

  async ngOnInit() {

      await this.route.paramMap.subscribe((params: ParamMap) => {
        this.collectionId = params.get("id")!;
      });

      if (!this.collectionId) {
        this.showCollections = true;
    }

    if (this.itemId) {

      this.item = (await this.itemService.getById(this.itemId))!;
      this.sanatizedHtml = this.sanitizeText(this.item["htmlContent"]);

      const that = this;
      let tinyTimeout = setTimeout(function () {
        if (typeof (tinymce) != "undefined") {
          tinymce.activeEditor.setContent(that.sanatizedHtml['changingThisBreaksApplicationSecurity']);
          clearTimeout(tinyTimeout)
        }
      }, 1000);
    }
    
    //this.areaViewModels = await this.areaService.getAll();
  }

  sanitizeText = (htmlContent: string): SafeHtml => {
    return this.sanitizer.bypassSecurityTrustHtml(htmlContent);
  }

  selectArea = async (areaId: string) => {
    this.modelListService = new ModelListService(this.http);
    this.modelListService
      .getCollectionsByAreaId(areaId)
      .then(function (collections) {});
  };

  selectCollection = async (value: string) => {
    let test = value;
  };

  onSubmit = async (data) => {
    this.item = data.value;
    const input = (document.querySelector("input[type=file]") as HTMLInputElement);
    if (input && input.files) this.files = input.files;

    this.item.HtmlContent = tinymce.activeEditor.getContent();

    var div = document.createElement('div');

    div.className = "tiny-content";

    div.innerHTML = this.item.HtmlContent.trim();

    const collection = div.getElementsByTagName("img");

    let imgParentEl: HTMLElement = new HTMLElement;
    if (collection.length > 0) {
      imgParentEl = ((collection[0] as HTMLImageElement).parentElement)!;

      imgParentEl.className = "tiny-img";
    }

    this.item.HtmlContent = div.innerHTML;

    this.item.Collection = (await this.collectionService
      .getById(data.value.collectionId ?? this.collectionId))!;

    if (this.files?.length) {
      const fileToUpload = this.files[0];

      const that = this;
      await this.uploadService
        .uploadFile(fileToUpload)
        .then(async (response: any) => {
          if (response && response.body) {
            that.item.FileViewModels = [ { Id: response.body }] as [IFile];

            await (that.item["id"]
              ? this.itemService.update("items", that.item)
              : this.itemService.create("items", that.item)
            ).then(() => {
              location.reload();
            });
          }
        });

    } else {
      let callMethod = !this.itemId
        ? this.itemService.create("items", this.item)
        : this.itemService.update("items", this.item);

      await callMethod.then(() => {
        location.reload();
      });
    }
  };
}
