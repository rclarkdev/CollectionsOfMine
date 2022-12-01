import { SafeHtml, SafeResourceUrl } from "@angular/platform-browser";
import { IBase } from "./base.interface";
import { ICollection } from "./collection.interface";
import { IFile } from "./file.interface";

export interface IItem {
  Base: IBase;
  Source: string;
  IframeSrc: string;
  CustomContent: string;
  HtmlContent: string;
  SanitizedHtml: SafeHtml;
  SanitizedIframe: SafeResourceUrl;
  CategoryViewModels: [IBase];
  Collection: ICollection;
  AttributeViewModels: [IBase];
  SelectedAttributes: [IBase];
  FileViewModels: [IFile];
  TimelineDate: Date;
}
