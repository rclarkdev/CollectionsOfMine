import { IBase } from "./base.interface";

export interface IFile {
  Base: IBase;
  Type: string;
  Base64: string;
  Bytes: string;
}
