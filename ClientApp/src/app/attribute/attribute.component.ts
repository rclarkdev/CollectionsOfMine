import { HttpClient } from "@angular/common/http";
import { Attribute } from "@angular/compiler";
import { Component, Input, OnInit } from "@angular/core";
import { ActivatedRoute, ParamMap, Router } from "@angular/router";
import { IAttribute } from "../interfaces/attribute.interface";
import { CommonService } from "../services/common.service";

@Component({
  selector: "app-attribute",
  templateUrl: "./attribute.component.html",
  styleUrls: ["./attribute.component.css"],
})
export class AttributeComponent implements OnInit {
  attribute: IAttribute;
  @Input() attributeId: string;
  attributesService: CommonService<IAttribute>;
  id: string;

  constructor(
    private router: Router,
    private http: HttpClient,
    private route: ActivatedRoute
  ) {
    this.attributesService = new CommonService(http);
  }

  async ngOnInit() {
    await this.route.paramMap.subscribe((params: ParamMap) => {
      this.id = params.get("id")!;
    });

    if (this.id) {
      this.attribute = (await this.attributesService.getById(this.id))!;
    }
  }

  onSubmit(data) {
    this.createOrUpdate(data.value);
  }

  createOrUpdate = async (attribute) => {
    await (attribute["id"]
      ? this.attributesService.update("attributes", attribute)
      : this.attributesService.create("attributes", attribute)
    ).then(() => {
      this.router.navigate(["attributes"]);
    });
  };
}
