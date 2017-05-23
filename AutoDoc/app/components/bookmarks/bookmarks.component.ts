import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { BookmarkService } from '../../services/bookmark.service';
import { DataService } from '../../services/data.service';

import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';
import { Hero } from '../../models/hero.type';

import { WebStorageModule, LocalStorageService } from "angular2-localstorage";
import { Router } from '@angular/router';

@Component({
    selector: 'bookmarks',
    //templateUrl: 'app/components/bookmarks/bookmarks.component.html',
    template: `<div>
<h3>Page One </h3>
  <input [(ngModel)]="hero.name" type="text" />
  <br />
	<h2>Your Bookmarks</h2>
        <ul>
        <li *ngFor="let bookmark of document.bookmarks">
            <span>{{bookmark.id}}</span> {{bookmark.name}}
            <input [(ngModel)]="bookmark.message" placeholder="New Value" />
        </li>
    </ul>
	<button (click)="uploadBookmarks(document.id)">Set</button>
</div>
  `,
    providers: [BookmarkService, DataService, Bookmark, Document, DocumentComponent]
})

export class BookmarkComponent implements OnInit, OnDestroy {
    //@Input() document: Document;
    //@Input() bookmarks: Bookmark[];
    document: Document;
    hero: Hero; 

    constructor(private bookmarkService: BookmarkService, public dataservice: DataService) {
    }

    ngOnInit() {
        this.document = this.dataservice.document;
        this.hero = this.dataservice.hero; 
    }

    ngOnDestroy() {  
    }

    uploadBookmarks() {
    }
}
