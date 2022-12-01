import { HttpClient } from '@angular/common/http';
import { AfterViewChecked, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IArea } from '../../interfaces/area.interface';
import { IBase } from '../../interfaces/base.interface';
import { ICollection } from '../../interfaces/collection.interface';
import { CommonService } from '../../services/common.service';
import { UploadService } from '../../services/upload.service';

@Component({
  selector: 'app-collection-form',
  templateUrl: './collection-form.component.html',
  styleUrls: ['./collection-form.component.css']
})
export class CollectionFormComponent implements OnInit, AfterViewChecked {

  @Output() emitRefreshCollections = new EventEmitter<boolean>();
  @Input() selectedArea: number;
  @Input() collection: ICollection;

  private areaViewModels: IArea[];
  private contentTypes: IBase[];
  private selectedContentTypes: IBase[] = [];
  private collectionsService: CommonService<ICollection>;
  private areasService: CommonService<IArea>;
  private contentTypesService: CommonService<IBase>;
  private files: FileList;
  private clickHandlersSet: boolean;

  constructor(
    private http: HttpClient,
    private uploadService: UploadService,
    private modalService: NgbModal) {
    this.collectionsService = new CommonService("collections", http);
    this.areasService = new CommonService("areas", http);
    this.contentTypesService = new CommonService("contentTypes", http);
  }

  async ngOnInit() {
    this.areaViewModels = await this.areasService.getAll();
    this.contentTypes = await this.contentTypesService.getAll();
  }

  ngAfterViewChecked() {

    if (!this.clickHandlersSet) {
      const btnDoc = document.getElementsByClassName('btn-Documents')[0];
      const btnPic = document.getElementsByClassName('btn-Pictures')[0];
      const btnVid = document.getElementsByClassName('btn-Videos')[0];
      const btnCus = document.getElementsByClassName('btn-Custom')[0];

      if (btnVid && btnPic && btnDoc && btnCus) {
        this.handleClick(btnDoc);
        this.handleClick(btnPic);
        this.handleClick(btnVid);
        this.handleClick(btnCus);
        this.clickHandlersSet = true;
      }
    }
    

  }

  handleClick(el: Element) {
    el.addEventListener('click', function onClick(event) {

      event.preventDefault();
      event.stopPropagation();

      console.log(event.target);

      const backgroundColor = (<HTMLButtonElement>event.target).style.backgroundColor;

      if (backgroundColor !== 'lightgreen') {
        (<HTMLButtonElement>event.target).style.backgroundColor = 'lightgreen';
        el.classList.add('selected');
      } else {
        (<HTMLButtonElement>event.target).style.backgroundColor = '#DEDEDE';
        el.classList.remove('selected');
      }
    });
  }

  async onSubmit(data) {

    this.collection = data.value;
    const input = (document.querySelector("input[type=file]") as HTMLInputElement);
    if (input) this.files = input.files;

    let fileId = 0;
    if (this.files?.length) {
      const fileToUpload = this.files[0];
      
      await this.uploadService
        .uploadFile(fileToUpload)
        .then(async (response: any) => {
          if (response && response.body) {
            fileId = response.body;
          }
        });
    }
    this.collection.FileId = fileId;
    this.collection.Area = {
      Base: { Id: data.value.selectedArea } } as IArea;
    this.createOrUpdate(this.collection);
  }

  createOrUpdate = async (collection) => {

    const contentTypeBtns = document.getElementsByClassName("content-btn");

    let that = this;
    if (contentTypeBtns) {
      Array.from(contentTypeBtns).forEach(function (element) {
        if (element.classList.contains("selected")) {
          that.selectedContentTypes.push(that.contentTypes.find(t => t['name'] === element['defaultValue']));
        }
      });

      if (this.selectedContentTypes) {
        collection.ContentTypes = this.selectedContentTypes;
      }
    }

    await (collection["id"]
      ? this.collectionsService.update(collection)
      : this.collectionsService.create(collection)
    ).then(() => {
      this.modalService.dismissAll();
      this.emitRefreshCollections.emit(true);
    });
  };


}
