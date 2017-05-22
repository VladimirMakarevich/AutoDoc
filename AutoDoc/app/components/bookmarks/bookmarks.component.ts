import { Component, OnInit } from '@angular/core';
import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';

@Component({
    selector: 'bookmarks-replace',
    templateUrl: 'app/components/bookmarks/bookmarks.component.html',
})

export class BookmarkComponent {

    constructor(private bookmark: Bookmark,
        private document: Document) {
    }

    uploadBookmarks() {

    }
}
