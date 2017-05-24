import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { BookmarkService } from "../../services/document/bookmark.service";
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Injectable } from '@angular/core';

import { Bookmark } from '../../Models/bookmark';
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';

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

    inputOptions = Array<Option>();
    data = Array<any>();
    settings: Settings;

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private bookmarkService: BookmarkService) {
    this.source = new LocalDataSource(this.data);
    }

    AddNewHeader(): void {
        this.settings.columns.push(new Column (this.newHeaderName, false));
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

export class Settings {
    columns = Array<Column>();
}

export class Column {
    constructor(title: string, filter: boolean) {
        this.title = title;
        this.filter = filter;
    }

    title: string;
    filter: boolean;
}