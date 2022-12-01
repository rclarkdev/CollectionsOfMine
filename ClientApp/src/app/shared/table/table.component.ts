import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { DataTableService } from "../../services/data-table.service";

@Component({
  selector: "app-table",
  templateUrl: "./table.component.html",
  styleUrls: ["./table.component.css"],
})
export class TableComponent implements OnInit {
  private fields: string[];
  @Input() rows: any[];

  @Output() editEvent: EventEmitter<any> = new EventEmitter();
  @Output() deleteEvent: EventEmitter<any> = new EventEmitter();

  editRow(row) {
    this.editEvent.emit(row.Id);
  }

  deleteRow(row) {
    this.deleteEvent.emit(row.Id);
  }

  dtOptions: DataTables.Settings = {};
  dtTrigger: any;

  constructor(private dataTableService: DataTableService) {}

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: "full_numbers",
      pageLength: 2,
      paging: false,
      searching: true,
    };

    this.dataTableService.dtTrigger.subscribe(
      (dtTrigger) => (this.dtTrigger = dtTrigger)
    );

    this.fields = Object.keys(this.rows[0]);
  }

  ngOnDestroy(): void {
    if (this.dtTrigger) this.dtTrigger.unsubscribe();
  }
}
