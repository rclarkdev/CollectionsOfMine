import { HttpClient } from "@angular/common/http";
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from "@angular/core";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { IArea } from "../interfaces/area.interface";
import { ICollection } from "../interfaces/collection.interface";
import { CommonService } from "../services/common.service";
import { AreaListComponent } from "./list/area-list.component";

@Component({
  selector: "app-area",
  templateUrl: "./area.component.html",
  styleUrls: ["./area.component.css"],
})
export class AreaComponent implements OnInit {
  private area: IArea;
  private collections: ICollection[] = [];
  private areasService: CommonService<IArea>;
  private collectionsService: CommonService<ICollection>;
  private isCreateOrEdit: boolean;

  @Input() createArea: boolean;
  @Input() areaId: string;

  @Output("refreshAreas") refreshAreas: EventEmitter<any> = new EventEmitter();
  @Output("area") areaEmitter: EventEmitter<any> = new EventEmitter();

  constructor(
    private router: Router,
    private http: HttpClient,
    private modalService: NgbModal
  ) {
    this.areasService = new CommonService("areas", http);
    this.collectionsService = new CommonService("collections", http);
  }

  async ngOnInit() {

    if (this.createArea) {
      this.isCreateOrEdit = true;
      this.areaId = null;
    }

    
    if (this.areaId) {
      this.isCreateOrEdit = true;
      this.area = await this.areasService.getById(this.areaId);
      this.areaEmitter.emit(this.area);
    }
  }

  emitRefreshAreas = () => {
    this.refreshAreas.emit();
  }
}
