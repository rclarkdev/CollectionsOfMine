import { Router } from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { UserService } from "../services/user.service";
import { CommonService } from "../services/common.service";
import { CollectionListComponent } from "../collection/list/collection-list.component";
import { Component, OnInit } from "@angular/core";
import { IArea } from "../interfaces/area.interface";

@Component({
  selector: "app-nav-menu",
  templateUrl: "./nav-menu.component.html",
  styleUrls: ["./nav-menu.component.css"],
})
export class NavMenuComponent implements OnInit {
  isExpanded = false;
  isLoggedIn = false;
  showAdminBoard = false;
  showModeratorBoard = false;
  areaService: CommonService<IArea>;
  userService: UserService;
  areas: IArea[];
  user: any;
  username: string;

  constructor(private router: Router, private http: HttpClient, private modalService: NgbModal) {
    this.areaService = new CommonService(http);
    this.userService = new UserService();
  }

  async ngOnInit() {
  this.userService.userObservable.subscribe(u => this.user = u);

    if (this.user) {
      this.isLoggedIn = true;
      this.username = this.user.firstName;
    }

    this.areas = (await this.areaService.getAll("areas"))!;
    this.areaService.setTypes(this.areas);

    if (this.areas && this.areas.length) {
      this.areas.forEach((area) => {
        this.router.config.push({
          path: area["name"]?.toLowerCase(),
          component: CollectionListComponent,
        });
      });
    }
  }

  open(content: any, createItem: boolean = false) {
    this.modalService.open(content).result.then(
      (result) => { },
      (reason) => { }
    );
  }

  logout = () => {

  }


  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }
}
