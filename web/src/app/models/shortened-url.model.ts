export class ShortenedUrl {
  constructor(
    public id: string,
    public shortenedUrl: string,
    public realUrl: string,
    public createdAt: Date
  ) {

  }
}
