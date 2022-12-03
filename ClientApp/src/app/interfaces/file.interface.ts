
export interface IFile {
  Id: number;
  IsDeleted: boolean;
  UpdatedOn: Date;
  CreatedOn: Date;
  Name: string;
  Description: string;
  Type: string;
  Base64: string;
  Bytes: string;
}
