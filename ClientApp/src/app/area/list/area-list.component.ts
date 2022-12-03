import { Component, EventEmitter, OnInit, Output, ViewChild } from "@angular/core";
import { Router, ActivatedRoute, ParamMap } from "@angular/router";
import { DataTableService } from "../../services/data-table.service";
import { IArea } from "../../interfaces/area.interface";
import { CommonService } from "../../services/common.service";
import { HttpClient } from "@angular/common/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { DataTableDirective } from "angular-datatables";
import { Subject } from "rxjs";

@Component({
  selector: "app-area-list",
  templateUrl: "./area-list.component.html",
  styleUrls: ["./area-list.component.css"],
})
export class AreaListComponent implements OnInit {
  areas: IArea[];
  viewAreas: object[] = [];
  area: string;
  showTable: boolean = true;
  areasService: CommonService<IArea>;
  response: { dbPath: "" };
  areaId: string;
  createArea: boolean;

  @ViewChild(DataTableDirective, { static: false })
  private dtElement: DataTableDirective;

  dtOptions: DataTables.Settings = {};

  dtTrigger: Subject<any> = new Subject();

  constructor(
    private router: Router,
    private http: HttpClient,
    private activeRoute: ActivatedRoute,
    private dataTableService: DataTableService,
    private modalService: NgbModal
  ) {
    this.areasService = new CommonService(http);
  }

  open(content: any, createArea: boolean = false) {
    this.createArea = createArea;
    this.modalService.open(content).result.then(
      (result) => { },
      (reason) => { }
    );
  }

  async ngOnInit() {

    this.activeRoute.paramMap.subscribe((params: ParamMap) => {
          this.area = params.get("area")!;
      });

    this.allAreas();

  }

  allAreas = async () => {
    const that = this;
    this.getAreas().then(function (areas) {
      that.areasService.setTypes(areas);
      that.areasService.typesObservable.subscribe((areas) => {
        that.areas = areas;
        that.dataTableService.setDtTrigger(areas);
        that.viewAreas.length = 0;
        that.areas.forEach(function (area) {
          let viewArea = {
            Id: area["id"],
            Name: area["name"],
            Description: area["description"],
            CreatedOn: area["createdOn"].split("T")[0],
          };

          that.viewAreas.push(viewArea);
        });
      });
    });
  }

  getAreas = async () => {
    return await this.areasService.getAll("areas");
  };

  refreshAreas = (e) => {
    this.allAreas();
  }
  
  edit = async (content, id) => {
    this.areaId = id;
    this.open(content);
  };

  delete = async (id) => {
    if (confirm("Are you sure?")) {
      await this.areasService.delete(id);
      this.refreshAreas(this);
      this.rerender();
    }
  };

  rerender(): void {
    this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
      dtInstance.destroy();
     // this.dtTrigger.next();
    });
  }
}
