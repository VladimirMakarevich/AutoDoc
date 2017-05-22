import { Component, Input, OnInit } from '@angular/core';
import { DocumentService } from '../../services/document.service';
import { BookmarkService } from '../../services/bookmark.service';
import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';

@Component({
    selector: 'bookmarks',
    templateUrl: 'app/components/bookmarks/bookmarks.component.html',
    providers: [DocumentService, BookmarkService]
})

export class BookmarkComponent {
    @Input() document: Document;

    constructor() {
    }

    uploadBookmarks() {

    }
}
