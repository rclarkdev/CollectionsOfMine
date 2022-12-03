import { IArea } from "./area.interface";

export interface ICollection {
  Id: number;
  IsDeleted: boolean;
  UpdatedOn: Date;
  CreatedOn: Date;
  Name: string;
  Description: string;
  //ContentTypes: [IBase];
  FileId: number;
  Area: IArea;
  SelectedArea: number;
}
