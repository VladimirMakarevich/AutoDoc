import { Component, Input, OnInit, OnDestroy, VERSION, NgModule } from '@angular/core';
import { BookmarkService } from '../../services/bookmark.service';
import { DataService } from '../../services/data.service';
import { Ng2SmartTableModule, LocalDataSource, ViewCell } from 'ng2-smart-table';

import { Bookmark } from '../../models/bookmarks/bookmark.type';
import { Document } from '../../models/document/document.type';
import { Table } from '../../models/table/table.type'

import { Router } from '@angular/router';

@Component({
    selector: 'bookmarks',
    templateUrl: 'app/components/bookmarks/bookmarks.component.html',
})

export class BookmarkComponent implements OnInit, OnDestroy {
    document: Document;
    private id: number;
    newColumn: any;
    inputOptions = Array<Option>();

    constructor(private bookmarkService: BookmarkService, public dataservice: DataService,
        private router: Router) {
    }

    public renderValue: any;

    @Input() value: any;

    example() {
        alert(this.renderValue);
    }

    ngOnInit() {
        this.document = this.dataservice.getDocument();
        this.inputOptions = Array<Option>();
        this.inputOptions.push(new Option(1, 'Text'));
        this.inputOptions.push(new Option(2, 'Table'));
        this.renderValue = this.value;
    }

    ngOnDestroy() {  
    }

    changeBookmarkType(bookmark: Bookmark): void {
        if (bookmark.type == 2) {
            bookmark.message = new Table();
        }
        if (bookmark.type == 1) bookmark.message;
    }


    addColumn(bookmark: Bookmark): void {
        let set = bookmark.message.settings;
        set.columns[this.newColumn] = { title: this.newColumn, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, set);
    }

    uploadBookmarks(id: number) {
        let data = this.getTable(this.document);
        this.bookmarkService.uploadBookmarks(data)                
            .then(id => {
                this.id = id;
                this.router.navigate(['/downdocument', id]);
        });
    }

    getTable(obj: any): Document {
        for (var i = 0; i < obj.bookmarks.length; i++) {
            if (obj.bookmarks[i].type == "2") {
                let dataTable = JSON.stringify(obj.bookmarks[i].message.data.data);
                obj.bookmarks[i].message = dataTable;
            }
        }

        return obj;
    }
}


export class Option {
    constructor(id: number, name: string) {
        this.id = id;
        this.name = name;
    }

    id: number;
    name: string;
}