import { Injectable } from '@angular/core';
import { ShortenedUrl } from '../models/shortened-url.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UrlsService {
  constructor(private _http: HttpClient) { }

  getAll(): ShortenedUrl[] {
    return [new ShortenedUrl('0', 'zxc', 'cxz', new Date())]
  }

  getById(): void { // ShortenedUrl

  }

  add(url: string) {

  }

  delete(id: string) {

  }
}
