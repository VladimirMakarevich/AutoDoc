import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { BookmarkService } from "../../services/document/bookmark.service";
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Injectable } from '@angular/core';

import { Bookmark, Table } from '../../Models/bookmark';
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';
import { EditableTableModule } from 'ng-editable-table';

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

    source: LocalDataSource;
    newHeaderName: string;
    headers = Array<string>();

    inputOptions = Array<Option>();
   
    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private bookmarkService: BookmarkService) {
    }

    changeBookmarkType(bookmark: Bookmark): void {
        if (bookmark.type == 2) {
            bookmark.message = new Table();
            bookmark.message.headers = new Array<string>();
            bookmark.message.data = new Array<Array<string>>();
        }
        if (bookmark.type == 1) bookmark.message;
    }

    addNewHeader(bookmark: Bookmark): void {
        bookmark.message.headers.push(this.newHeaderName);
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.headers.push(this.newHeaderName);
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.data
    }

    uploadNewValues(): void {
        this.bookmarkService.postData(this.bookmarks).subscribe((ans : string) => {
            //console.log(ans);
            this.router.navigate(['./download', this.id]);
        });
    }
        
    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
           this.id = params['id'];
           if (this.id != '') {
               this.bookmarkService.getData(this.id).subscribe((bookmarks: Bookmark[]) => {
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

