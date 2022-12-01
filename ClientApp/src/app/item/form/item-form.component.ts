import { HttpClient } from '@angular/common/http';
import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IBase } from '../../interfaces/base.interface';
import { ICollection } from '../../interfaces/collection.interface';
import { IItem } from '../../interfaces/item.interface';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.css']
})
export class ItemFormComponent implements OnInit {

  @Input() item: IItem;
  @Input() collection: ICollection;

  private contentTypes: [IBase];
  private includeCustomContent: boolean;
  private includePicteres: boolean;
  private includeVideos: boolean;
  private includeDocuments: boolean;


  constructor() { }

  async ngOnInit() {
   
    this.contentTypes = this.collection['contentTypes'];

    if (this.contentTypes.find(c => c['name'] === "Custom")) this.includeCustomContent = true;
    if (this.contentTypes.find(c => c['name'] === "Pictures")) this.includePicteres = true;
    if (this.contentTypes.find(c => c['name'] === "Videos")) this.includeVideos = true;
    if (this.contentTypes.find(c => c['name'] === "Documents")) this.includeDocuments = true;

    debugger;
  }
}
