import { IBase } from "./base.interface";
import { ICollection } from "./collection.interface";
import { IItem } from "./item.interface";

export interface IArea {
  Base: IBase;
  Items: [IItem];
  Collections: [ICollection];
}
