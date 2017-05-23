import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { BookmarkService } from '../../services/bookmark.service';
import { DataService } from '../../services/data.service';

import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';

//import { WebStorageModule, LocalStorageService } from 'angular2-local-storage';
import { Router } from '@angular/router';

@Component({
    selector: 'bookmarks',
    templateUrl: 'app/components/bookmarks/bookmarks.component.html',
})

export class BookmarkComponent implements OnInit, OnDestroy {
    document: Document;

    constructor(private bookmarkService: BookmarkService, public dataservice: DataService) {
    }

    ngOnInit() {
        this.document = this.dataservice.getDocument();
    }

    ngOnDestroy() {  
    }

    uploadBookmarks(id: number) {
        this.bookmarkService.uploadBookmarks(this.document)
    }
}
