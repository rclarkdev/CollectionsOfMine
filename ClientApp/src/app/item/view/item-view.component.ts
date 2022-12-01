import { Component, Input, OnInit } from "@angular/core";
import { IItem } from '../../interfaces/item.interface';

@Component({
  selector: 'app-item-view',
  templateUrl: './item-view.component.html',
  styleUrls: ['./item-view.component.css']
})
export class ItemViewComponent implements OnInit {

  @Input() item: IItem;

  constructor() { }

  ngOnInit(): void {
  }
}
