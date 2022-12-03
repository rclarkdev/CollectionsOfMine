import { Component, OnInit } from "@angular/core";
import { Router, ActivatedRoute } from "@angular/router";
import { DataTableService } from "../../services/data-table.service";
import { IAttribute } from "../../interfaces/attribute.interface";
import { CommonService } from "../../services/common.service";
import { HttpClient } from "@angular/common/http";
import { IArea } from "../../interfaces/area.interface";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: "app-attributes",
  templateUrl: "./attributes.component.html",
  styleUrls: ["./attributes.component.css"],
})
export class AttributesComponent implements OnInit {
  attributes: IAttribute[];
  viewAttributes: object[] = [];
  showTable: boolean = true;
  attributesTitle: string;
  attributesService: CommonService<IAttribute>;
  response: { dbPath: "" };
  attributeId: string;

  constructor(
    private router: Router,
    private http: HttpClient,
    private activeRoute: ActivatedRoute,
    private dataTableService: DataTableService,
    private modalService: NgbModal
  ) {
    this.attributesService = new CommonService(http);
  }

  open(content: any) {
    this.modalService.open(content).result.then(
      (result) => {},
      (reason) => {}
    );
  }

  async ngOnInit() {
    const that = this;

    this.getAttributes().then(function (attributes) {

      that.attributesService.setTypes(attributes);

      that.attributesService.typesObservable.subscribe(async (attributes) => {
        if (that.activeRoute?.routeConfig?.path === "attributes") {
          that.attributesTitle = " Attributes";
          that.dataTableService.setDtTrigger(attributes);

          that.attributes.forEach(async function (attribute) {
            let viewAttribute = {
              Id: attribute["id"],
              Name:
                '<a name="attribute" href="/items?attribute=' +
                attribute["name"] +
                '">' +
                attribute["name"] +
                "</a>",
              Description: attribute["description"],
              CreatedOn: attribute["createdOn"],
            };
            that.viewAttributes.push(viewAttribute);
          });
        }
      });
    });
  }

  getAttributes = async () => {
    return await this.attributesService.getAll("attributes");
  };

  edit = async (content, id) => {
    this.attributeId = id;
    this.open(content);
  };

  delete = async (id) => {
    if (confirm("Are you sure?")) {
      await this.attributesService.delete(id);
      location.reload();
    }
  };

  uploadFinished = (event) => {
    this.response = event;
  };
}
