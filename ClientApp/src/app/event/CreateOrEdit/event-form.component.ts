import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IBase } from '../../interfaces/base.interface';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-event-form',
  templateUrl: './event-form.component.html',
  styleUrls: ['./event-form.component.css']
})
export class EventFormComponent implements OnInit {

  private eventsService: CommonService<IBase>;

  constructor(
    private router: Router,
    private http: HttpClient,
    private modalService: NgbModal) {
      this.eventsService = new CommonService("events", http);
    }

  ngOnInit(): void {
    
  }


  onSubmit(data) {
    this.createOrUpdate(data.value);
  }

  createOrUpdate = async (event) => {
    await (event["id"]
      ? this.eventsService.update(event)
      : this.eventsService.create(event)
    ).then(() => {
      this.modalService.dismissAll();
      //this.refreshTimeline.emit(true);
    });
  };
}
