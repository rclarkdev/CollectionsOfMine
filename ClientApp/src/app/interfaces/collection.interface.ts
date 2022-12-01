import { IArea } from "./area.interface";
import { IBase } from "./base.interface";

export interface ICollection {
  Base: IBase;
  ContentTypes: [IBase];
  FileId: number;
  Area: IArea;
  SelectedArea: number;
}
