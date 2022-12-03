import { HttpClient } from "@angular/common/http";
import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { ICollection } from "../interfaces/collection.interface";
import { CommonService } from "../services/common.service";
import { IArea } from "../interfaces/area.interface";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-collection",
  templateUrl: "./collection.component.html",
  styleUrls: ["./collection.component.css"],
})
export class CollectionComponent implements OnInit {
  collection: ICollection;
  collectionsService: CommonService<ICollection>;
  areaViewModels: IArea[];
  areasService: CommonService<IArea>;
  id: string;
  isCreateOrEdit: boolean;

  @Input() selectedArea: number;
  @Input() collectionId: number;
  @Input() createCollection: boolean;

  @Output("refreshCollections") refreshCollections: EventEmitter<any> = new EventEmitter();
  @Output("collection") collectionEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    private router: Router,
    private http: HttpClient,
    private route: ActivatedRoute,
    private modalService: NgbModal
  ) {
    this.collectionsService = new CommonService(http);
    this.areasService = new CommonService(http);
  }

  async ngOnInit() {

    if (this.createCollection) {
      this.isCreateOrEdit = true;
      this.collectionId = 0;
    }

    if (this.collectionId) {
      this.isCreateOrEdit = true;
      this.collection = (await this.collectionsService.getById(this.collectionId))!;
      this.selectedArea = this.collection?.['area']?.['id'] ?? this.selectedArea;
      this.collectionEmitter.emit(this.collection);
    }
  }

  emitRefreshCollections = (e) => {
    this.refreshCollections.emit();
  }

  
}
