import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { BookmarkService } from "../../services/document/bookmark.service";
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Injectable, ApplicationRef, NgZone } from '@angular/core';

import { Bookmark, Table, Settings, Header } from '../../Models/bookmark';
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';
import { EditableTableModule } from 'ng-editable-table';
import { EditableTableService } from 'ng-editable-table';

import { FileUploader } from 'ng2-file-upload';
import { File } from '../../Models/file';

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
        if (bookmark.type == 2) bookmark.message = new Table();
        if (bookmark.type == 1) bookmark.message = '';
        if (bookmark.type == 3) bookmark.message = new File();
    }

    addNewHeader(bookmark: Bookmark): void {     
        let buf = bookmark.message.settings;
        buf.columns[this.newHeaderName] = { title: this.newHeaderName, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, buf);
    }

    fileSelected(fileInput: any, bookmark: Bookmark) {
        if (fileInput.target.files && fileInput.target.files[0]) {
            bookmark.message.fileContents = fileInput.target.files[0];
            bookmark.message.fileDownloadName = fileInput.target.files[0].name;
        }
    }

    uploadNewValues(): void {
        console.log(this.bookmarks);
        
        this.bookmarkService.postData(this.bookmarks).subscribe((ans: string) => {
            this.router.navigate(['./download', this.id]);
        });
    }

    prepareNewValues(): void {

        let bufIterator = new Array<number>();

        let allPromiseOperations: number = this.bookmarks.filter(el => el.type == 3).length;
        let donePromiseOperations: number = 0;

        let i = 0;
        while (i < this.bookmarks.length) {

            if (this.bookmarks[i].type == 2) {
                let dataTable = this.bookmarks[i].message.data.data;
                this.bookmarks[i].message.data = dataTable;
            }

            if (this.bookmarks[i].type == 3) {
                bufIterator.push(i);

                this.bookmarkService.postImageFile(this.bookmarks[i].message.fileContents).then((name: string) => {
                    if (name != null) {

                        let buf = new Bookmark();

                        buf.id = this.bookmarks[bufIterator[0]].id;
                        buf.name = this.bookmarks[bufIterator[0]].name;
                        buf.type = 3;
                        buf.message = name;

                        //this.bookmarks[bufIterator[0]].message = null;
                        //this.bookmarks[bufIterator[0]].message = new String(name);
                        //this.bookmarks[bufIterator[0]].message = name;

                        this.bookmarks[bufIterator[0]] = null;
                        this.bookmarks[bufIterator[0]] = buf;

                        bufIterator.splice(0, 1);
                    }

                    donePromiseOperations++;  
                    if (donePromiseOperations == allPromiseOperations && i >= this.bookmarks.length) this.uploadNewValues();

                });
            }

            if (this.bookmarks[i].type == 4) {
                let dataTable = this.bookmarks[i].message.data.data;
                this.bookmarks[i].message.data = dataTable;
            }

            i++;
        }

        if (donePromiseOperations == allPromiseOperations) this.uploadNewValues();
    }

    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
           this.id = params['id'];
           if (this.id != '') {
               this.bookmarkService.getData(this.id).subscribe((bookmarks: Bookmark[]) => {

                   let bufIterator = new Array<number>();

                   for (var i = 0; i < bookmarks.length; i++) {
                       if (bookmarks[i].type == 2) {
                           let buf = new Table();
                           buf.settings = bookmarks[i].message.settings; 
                           buf.data.load(bookmarks[i].message.data);
                           bookmarks[i].message = buf;
                       }
                       if (bookmarks[i].type == 3) {
                           bufIterator.push(i);
                           this.bookmarkService.getImageFile(bookmarks[i].message).then((file: File) => {
                               if (file != null) {
                                   bookmarks[bufIterator[0]].message = new File();
                                   bookmarks[bufIterator[0]].message = file;
                                   bufIterator.splice(0, 1);
                               }
                           });
                       }
                       if (bookmarks[i].type == 4) {
                           let buf = new Table();
                           console.log(bookmarks[i].message.settings);
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
        this.inputOptions.push(new Option(3, 'Image'));
        this.inputOptions.push(new Option(4, 'Expanded Table'));
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

