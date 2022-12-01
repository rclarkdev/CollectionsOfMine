import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AreaComponent } from "./area.component";
import { AreaListComponent } from "./list/area-list.component";
import { CollectionComponent } from "../collection/collection.component";
import { CollectionListComponent } from "../collection/list/collection-list.component";
import { RouterModule } from "@angular/router";
import { FormsModule } from "@angular/forms";
import { TilesComponent } from "../shared/tiles/tiles.component";
import { TableComponent } from "../shared/table/table.component";
import { NgbModule } from "@ng-bootstrap/ng-bootstrap";
import { ItemComponent } from "../item/item.component";
import { ItemListComponent } from "../item/list/item-list.component";
import { AttributeComponent } from "../attribute/attribute.component";
import { AttributesComponent } from "../attribute/list/attributes.component";
import { UploadComponent } from "../upload/upload.component";
import { DataTablesModule } from "angular-datatables";
import { ReactiveFormsModule } from "@angular/forms";
import { NgSelectModule } from "@ng-select/ng-select";
import { EditorModule } from "@tinymce/tinymce-angular";
import { TinymceComponent } from "../shared/tinymce/tinymce.component";
import { IframeComponent } from "../shared/iframe/iframe.component";
import { ItemViewComponent } from "../item/view/item-view.component";
import { MatExpansionModule } from '@angular/material/expansion';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AreaFormComponent } from './form/area-form.component';
import { AreaViewComponent } from './view/area-view.component';
import { ItemFormComponent } from '../../app/item/form/item-form.component';
import { CollectionFormComponent } from '../../app/collection/form/collection-form.component';
import { CollectionViewComponent } from '../../app/collection/view/collection-view.component';
import { EventComponent } from '../event/event.component';
import { TimelineFormComponent } from '../timeline/CreateOrEdit/timeline-form.component';
import { EventFormComponent } from '../event/CreateOrEdit/event-form.component';
import { TimelineComponent } from "./../timeline/timeline.component";



@NgModule({
  declarations: [
    EventFormComponent,
    EventComponent,
    TimelineFormComponent,
    ItemComponent,
    UploadComponent,
    ItemListComponent,
    AreaComponent,
    AreaListComponent,
    CollectionListComponent,
    CollectionComponent,
    TilesComponent,
    TableComponent,
    AttributeComponent,
    AttributesComponent,
    TinymceComponent,
    IframeComponent,
    ItemViewComponent,
    AreaFormComponent,
    AreaViewComponent,
    ItemFormComponent,
    CollectionFormComponent,
    CollectionViewComponent
  ],
  imports: [
    MatFormFieldModule,
    MatInputModule,
    MatExpansionModule,
    CommonModule,
    FormsModule,
    NgbModule,
    FormsModule,
    ReactiveFormsModule,
    NgSelectModule,
    DataTablesModule,
    EditorModule,
    RouterModule.forRoot([
      { path: "area/:id", component: AreaComponent },
      { path: "area", component: AreaComponent },
      { path: "areas", component: AreaListComponent },
      { path: "collection/:id", component: CollectionComponent },
      { path: "collection", component: CollectionComponent },
      { path: "collections", component: CollectionListComponent },
      { path: "item/:id", component: ItemComponent },
      { path: "item", component: ItemComponent },
      { path: "items", component: ItemListComponent },
      { path: "attribute/:id", component: AttributeComponent },
      { path: "attribute", component: AttributeComponent },
      { path: "attributes", component: AttributesComponent },
      { path: "event", component: EventComponent },
      { path: "timeline", component: TimelineComponent }
    ]),
  ],
  bootstrap: [AreaComponent],
  exports: [],
})
export class AreaModule {}
