export class BaseParams {
  pageSize: number;
  pageNumber: number;

  constructor() {
    this.pageNumber = 1;
    this.pageSize = 12;
  }
}
