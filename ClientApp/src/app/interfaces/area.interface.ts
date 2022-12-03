import { ICollection } from "./collection.interface";
import { IItem } from "./item.interface";

export interface IArea {
  Id: number;
  IsDeleted: boolean;
  UpdatedOn: Date;
  CreatedOn: Date;
  Name: string;
  Description: string;
  Items: [IItem];
  Collections: [ICollection];
}
