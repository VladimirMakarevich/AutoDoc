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
    //mySettings: any;
    
    inputOptions = Array<Option>();

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private bookmarkService: BookmarkService,
        private tableService: EditableTableService,
        private ref: ApplicationRef,
        private ngZone: NgZone) {

        //this.mySettings = new Settings();
    }

    changeBookmarkType(bookmark: Bookmark): void {
        if (bookmark.type == 2) {
            bookmark.message = new Table();
            //bookmark.message.headers = new Array<string>();
            //bookmark.message.data = new Array<Array<string>>();

            /*bookmark.message.headers = ['qqq', 'www', 'zzz'];
            bookmark.message.data = [
                ['qqq', 'www', 'zzz'],
                ['qqq', 'www', 'zzz'],
                ['qqq', 'www', 'zzz']
            ];*/

        }
        if (bookmark.type == 1) bookmark.message;
    }

    addNewHeader(bookmark: Bookmark): void {
        /*this.ngZone.run(() => {
            //bookmark.message.headers.push(this.newHeaderName);
            //bookmark.message.data.push(new Array<string>());
        });*/
        let buf = bookmark.message.settings;
        buf.columns[this.newHeaderName] = { title: this.newHeaderName, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, buf);

        buf.dispose();
        //this.ref.tick();
        //NgZone.run(() => this.currentUser.next(user));
        //this.tableService.tableHeadersObjects.push(this.newHeaderName);
        //this.tableService.tableHeadersObjects.values.apply();
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.headers.push(this.newHeaderName);
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.data
    }

    uploadNewValues(): void {
        console.log(this.bookmarks[0].message);
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

