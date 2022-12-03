import { Component, Input, OnInit, ViewEncapsulation } from "@angular/core";
import { Router, ActivatedRoute, ParamMap } from "@angular/router";
import { DataTableService } from "../../services/data-table.service";
import { ICollection } from "../../interfaces/collection.interface";
import { CommonService } from "../../services/common.service";
import { HttpClient } from "@angular/common/http";
import { IArea } from "../../interfaces/area.interface";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { ModelListService } from "../../services/model-list.service";
import { IFile } from "../../interfaces/file.interface";

declare const Buffer: any;

@Component({
  selector: "app-collection-list",
  templateUrl: "./collection-list.component.html",
  styleUrls: ["./collection-list.component.css"],
})
export class CollectionListComponent implements OnInit {
  collections: ICollection[];
  viewCollections: object[] = [];
  showTable: boolean = true;
  collectionsTitle: string;
  collectionsService: CommonService<ICollection>;
  areasService: CommonService<IArea>;
  filesService: CommonService<IFile>;
  modelListService: ModelListService;
  response: { dbPath: "" };
  selectedArea: number;
  collectionId: number;
  createCollection: boolean;

  constructor(
    private router: Router,
    private http: HttpClient,
    private activeRoute: ActivatedRoute,
    private dataTableService: DataTableService,
    private modalService: NgbModal
  ) {
    this.collectionsService = new CommonService(http);
    this.areasService = new CommonService(http);
    this.filesService = new CommonService(http);
  }

  open(content: any, createCollection: boolean = false) {
    this.createCollection = createCollection;
    this.modalService.open(content).result.then(
      (result) => {},
      (reason) => {}
    );
  }

  async ngOnInit() {
    this.getCollections();
  }

  selectedAreaCollections = () => {
    this.activeRoute.paramMap.subscribe(async (params: ParamMap) => {
      this.selectedArea = Number(params.get("id"));
      if (this.selectedArea) {
        let area = await this.areasService.getById(this.selectedArea);

        if (area) {
          this.collectionsTitle = area["name"];
          this.collections = area["collections"];
          this.viewCollections.length = 0;
          const that = this;
          this.collections.forEach(async function (collection) {

            const fileId = collection["fileId"];
            let imgSrc = "";
            await that.filesService.getById(fileId).then(function (file) {
              if (file) {
                imgSrc = 'data:image/jpeg;base64,' + file['base64'];
              }
            });

            let viewcollection = {
              id: collection["id"],
              name: collection["name"],
              description: collection["description"],
              imgSrc: imgSrc
            };
            that.viewCollections.push(viewcollection);
          });
        }
      }
    });
  }

  allCollections = async () => {
    const that = this;
    await this.collectionsService.getAll("collections").then(function (collections) {
      that.collectionsService.setTypes(collections);
      that.collectionsService.typesObservable.subscribe((collections) => {
        that.collections = collections;
        that.dataTableService.setDtTrigger(collections);
        that.collectionsTitle = "Collections";
        that.viewCollections.length = 0;
        that.collections.forEach(function (collection) {
          let viewCollection = {
            Id: collection["id"],
            Name:
              '<a name="collection" href="/collection/' +
              collection["id"] +
              '">' +
              collection["name"] +
              "</a>",
            Description: collection["description"],
            CreatedOn: collection["createdOn"],
          };

          that.viewCollections.push(viewCollection);
        });
      });
    });
}

  getCollections = async () => {
    if (this.activeRoute?.routeConfig?.path === "collections") {
     this.allCollections();
    } else {
      this.showTable = false;
      this.modelListService = new ModelListService(this.http);
      this.selectedAreaCollections();
    }
  };

  refreshCollections = async (e) => {
    await this.getCollections();
  }

  edit = async (content, id) => {
    this.collectionId = id;
    this.open(content);
  };

  delete = async (id) => {
    if (confirm("Are you sure?")) {
      await this.collectionsService.delete(id);
      this.getCollections();
    }
  };
}
