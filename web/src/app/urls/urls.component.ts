import { Component, OnInit } from '@angular/core';
import { ShortenedUrl } from '../models/shortened-url.model';
import { UrlsService } from '../services/urls.service';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-urls',
  templateUrl: './urls.component.html',
  styleUrls: ['./urls.component.css']
})
export class UrlsComponent implements OnInit {
  urls: ShortenedUrl[] = [];
  isAddNewMode: boolean = false;
  newUrl: string = '';

  constructor(
    private _urls: UrlsService,
    private _auth: AuthService
  ) {

  }

  ngOnInit(): void {
    this.urls = this._urls.getAll();
  }

  onAddNewUrl(): void {
    console.log(this.newUrl)

    this._urls.add(this.newUrl);

    this.isAddNewMode = false;
    this.newUrl = '';
  }

  onDelete(id: string) {
    this._urls.delete(id);
  }

  onLogout(): void {
    this._auth.logout();
  }
}
