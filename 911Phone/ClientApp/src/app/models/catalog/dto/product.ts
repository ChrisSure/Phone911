import { Storage } from "../../shop/storage";

export class Product {
  public Id: number;
  public Title: string;
  public Image: string;
  public Price: number;
  public storages: Storage[];
}
