import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { BookmarkService } from "../../services/document/bookmark.service";
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Injectable, ApplicationRef, NgZone } from '@angular/core';

import { Bookmark, Table, Settings, Header } from '../../Models/bookmark';
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';
import { EditableTableModule } from 'ng-editable-table';
import { EditableTableService } from 'ng-editable-table';

@Component(({
    selector: 'bookmark-component',
    templateUrl: 'app/components/document/bookmark.component.html',
    providers: [BookmarkService]
}) as any)


@Injectable()
export class BookmarkComponent implements OnInit {

    bookmarks: Bookmark[];
    errorMessage: string;
    id: any;

    newHeaderName: string;
    
    inputOptions = Array<Option>();

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private bookmarkService: BookmarkService,
        private tableService: EditableTableService,
        private ref: ApplicationRef,
        private ngZone: NgZone) {
    }

    changeBookmarkType(bookmark: Bookmark): void {
        if (bookmark.type == 2) {
            bookmark.message = new Table();
        }
        if (bookmark.type == 1) bookmark.message = '';
    }

    addNewHeader(bookmark: Bookmark): void {     
        let buf = bookmark.message.settings;
        buf.columns[this.newHeaderName] = { title: this.newHeaderName, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, buf);
    }

    uploadNewValues(): void {

        for (var i = 0; i < this.bookmarks.length; i++) {
            if (this.bookmarks[i].type == 2) {
                let dataTable = this.bookmarks[i].message.data.data;
                this.bookmarks[i].message.data = dataTable;
            }
        }

        this.bookmarkService.postData(this.bookmarks).subscribe((ans : string) => {
            this.router.navigate(['./download', this.id]);
        });
    }
        
    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
           this.id = params['id'];
           if (this.id != '') {
               this.bookmarkService.getData(this.id).subscribe((bookmarks: Bookmark[]) => {

                   for (var i = 0; i < bookmarks.length; i++) {
                       if (bookmarks[i].type == 2) {
                           let buf = new Table();
                           console.log(bookmarks[i].message);
                           buf.settings = bookmarks[i].message.settings; 
                           buf.data.load(bookmarks[i].message.data);
                           bookmarks[i].message = buf;
                       }
                   }

                   this.bookmarks = bookmarks;
               });
            }
        });

        this.inputOptions = Array<Option>();
        this.inputOptions.push(new Option(1, 'Text'));
        this.inputOptions.push(new Option(2, 'Table'));
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

