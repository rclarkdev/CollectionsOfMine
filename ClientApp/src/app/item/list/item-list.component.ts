import { Component, OnDestroy, OnInit } from "@angular/core";
import { Subject } from "rxjs";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { DataTableService } from "../../services/data-table.service";
import { IItem } from "../../interfaces/item.interface";
import { HttpClient } from "@angular/common/http";
import { NgbModal, NgbModalOptions } from "@ng-bootstrap/ng-bootstrap";
import { ModelListService } from "../../services/model-list.service";
import { ICollection } from "../../interfaces/collection.interface";
import { CommonService } from "../../services/common.service";

@Component({
  selector: "app-item-list",
  templateUrl: "./item-list.component.html",
  styleUrls: ["./item-list.component.css"],
})
export class ItemListComponent implements OnInit {
  private items: IItem[];
  private collectionId: string;
  private collection: ICollection;
  private viewItems: object[] = [];
  private showTable: boolean = true;
  private editItem: boolean = true;
  private itemService: CommonService<IItem>;
  private collectionService: CommonService<ICollection>;
  private modelListService: ModelListService;
  private itemId: string;
  private areaId: string;
  private itemsTitle: string;
  private createOrEditItem: boolean;
  modalOptions: NgbModalOptions;

  constructor(
    private router: Router,
    private http: HttpClient,
    private activeRoute: ActivatedRoute,
    private dataTableService: DataTableService,
    private modalService: NgbModal
  ) {
    this.itemService = new CommonService("items", http);
    this.collectionService = new CommonService("collections", http);

    this.modalOptions = {
      size: 'lg'
    }
  }

  open(content: any) {
    if (!this.editItem) this.itemId = null;
    this.modalService.open(content, this.modalOptions).result.then(
      (result) => {},
      (reason) => {}
    );

    this.editItem = false;
  }

  close() {
    this.modalService.dismissAll();
  }

  async ngOnInit() {

    debugger;
    let collectionName = this.getURLParameter("collection");
    //const that = this;

    if (this.activeRoute.routeConfig.path === "items") {

      this.itemsTitle = "Items";

      await this.itemService.getAll().then(function (items) {
        this.itemService.typesObservable.subscribe(async (items) => {

          this.items = items;

          this.dataTableService.setDtTrigger(items);

          this.items.forEach(async function (item){
            let viewItem = {
              Id: item["id"],
              Name: item["name"],
              Description: item["description"],
              IsDeleted: item["isDeleted"],
              CreatedOn: item["createdOn"],
              Source: item["source"],
            };

            this.viewItems.push(viewItem);

            if (collectionName) {
              this.viewItems = this.viewItems.filter(function (viewItem) {
                return (
                  viewItem[
                    "collections"
                  ][0].collectionViewModel.name.toLocaleLowerCase() === collectionName
                );
              });
            }
          });
        });
        this.itemService.setTypes(items);
      });
    }
    else {
      this.showTable = false;
      this.modelListService = new ModelListService("items", this.http);

      await this.activeRoute.paramMap.subscribe(async (params: ParamMap) => {
        this.viewItems.length = 0;
        this.collectionId = params.get("id");
      });
      if (this.collectionId) {
        this.collection = await this.collectionService.getById(this.collectionId);

        let items = this.collection["items"];
        this.itemsTitle = this.collection["name"];
        this.areaId = this.collection["area"]["id"];

          items.forEach(function (item) {
            let viewItem = {
              id: item["id"],
              name: item["name"],
              description: item["description"],
            };
            this.viewItems.push(viewItem);
          });
        }
      
    }
  }

  edit = async (content, id) => {
    this.editItem = true;
    this.itemId = id;
    this.open(content);
  };

  delete = async (id) => {
    if (confirm("Are you sure?")) {
      await this.itemService.delete(id);
      location.reload();
    }
  };

  // TODO: put in a helper or extension
  getURLParameter = (name) => {
    return (
      decodeURIComponent(
        (new RegExp("[?|&]" + name + "=" + "([^&;]+?)(&|#|;|$)").exec(
          location.search
        ) || [null, ""])[1].replace(/\+/g, "%20")
      ) || null
    );
  };
}
