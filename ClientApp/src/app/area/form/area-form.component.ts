import { HttpClient } from '@angular/common/http';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IArea } from '../../interfaces/area.interface';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-area-form',
  templateUrl: './area-form.component.html',
  styleUrls: ['./area-form.component.css']
})
export class AreaFormComponent implements OnInit {

  @Output() emitRefreshAreas = new EventEmitter<boolean>();

  @Input() area: IArea;

  private areasService: CommonService<IArea>;

  constructor(
    private router: Router,
    private http: HttpClient,
    private modalService: NgbModal
  ) {
    this.areasService = new CommonService(http);
  }

  ngOnInit(): void {}

  onSubmit(data) {
    this.createOrUpdate(data.value);
  }

  createOrUpdate = async (area) => {
    await (area["id"]
      ? this.areasService.update("areas", area)
      : this.areasService.create("areas", area)
    ).then(() => {
      this.router.navigate(["areas"]);
      this.modalService.dismissAll();
      this.emitRefreshAreas.emit(true);
    });
  };

}
