import { Component, OnInit } from "@angular/core";
import { CommonService } from "../services/common.service";
import { IArea } from "../interfaces/area.interface";
import { Options, LabelType } from "@angular-slider/ngx-slider";
import { IItem } from "../interfaces/item.interface";
import { HttpClient } from "@angular/common/http";
import { NgbModal } from "@ng-bootstrap/ng-bootstrap";
import { IBase } from "../interfaces/base.interface";

@Component({
  selector: "app-timeline",
  templateUrl: "./timeline.component.html",
  styleUrls: ["./timeline.component.css"],
})
export class TimelineComponent implements OnInit {
  private collectionService: CommonService<IArea>;
  private eventService: CommonService<IBase>;

  dateRange: Date[] = this.customDateRange();
  value: number = this.dateRange[0].getTime();

  timelineEvents: IItem[];

  constructor(private http: HttpClient, private modalService: NgbModal) {
    this.eventService = new CommonService("events", http);

  }

  getEvents = () => {
    this.eventService.getAll().then(function (events) { // TODO: paging
      this.timelineEvents = events.filter(function (events) {
        debugger;
      });
    });
  }

  open(content: any, createArea: boolean = false) {
    this.modalService.open(content).result.then(
      (result) => { },
      (reason) => { }
    );
  }

  options: Options = {
    stepsArray: this.dateRange.map((date: Date) => {
      return { value: date.getTime() };
    }),
    translate: (value: number, label: LabelType): string => {
      return new Date(value).toDateString();
    },
  };

  customDateRange(): Date[] {
    const dates: Date[] = [];
    for (let i: number = 1; i <= 31; i++) {
      dates.push(new Date(2021, 6, i));
    }
    return dates;
  }

  ngOnInit(): void {
    this.getEvents();
  }
}
