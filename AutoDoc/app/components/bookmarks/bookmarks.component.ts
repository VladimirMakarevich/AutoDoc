import { Component, Input, OnInit, OnDestroy } from '@angular/core';
import { BookmarkService } from '../../services/bookmark.service';
import { DataService } from '../../services/data.service';
import { Ng2SmartTableModule } from 'ng2-smart-table';

import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';

import { Router } from '@angular/router';

@Component({
    selector: 'bookmarks',
    templateUrl: 'app/components/bookmarks/bookmarks.component.html',
})

export class BookmarkComponent implements OnInit, OnDestroy {
    document: Document;
    private id: number;

    constructor(private bookmarkService: BookmarkService, public dataservice: DataService, private router: Router) {
    }

    settings = {
        columns: {
            id: {
                title: 'ID'
            },
            name: {
                title: 'Full Name'
            },
            username: {
                title: 'User Name'
            },
            email: {
                title: 'Email'
            }
        }
    };

    ngOnInit() {
        this.document = this.dataservice.getDocument();
    }

    ngOnDestroy() {  
    }

    uploadBookmarks(id: number) {
        this.bookmarkService.uploadBookmarks(this.document)                
            .then(id => {
                this.id = id;
                this.router.navigate(['/downdocument', id]);
        });
    }
}
