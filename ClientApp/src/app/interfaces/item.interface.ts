import { SafeHtml, SafeResourceUrl } from "@angular/platform-browser";
import { ICollection } from "./collection.interface";
import { IFile } from "./file.interface";

export interface IItem {
  Id: number;
  IsDeleted: boolean;
  UpdatedOn: Date;
  CreatedOn: Date;
  Name: string;
  Description: string;
  Source: string;
  IframeSrc: string;
  CustomContent: string;
  HtmlContent: string;
  SanitizedHtml: SafeHtml;
  SanitizedIframe: SafeResourceUrl;
  //CategoryViewModels: [IBase];
  Collection: ICollection;
  //AttributeViewModels: [IBase];
  //SelectedAttributes: [IBase];
  FileViewModels: [IFile];
  TimelineDate: Date;
}
