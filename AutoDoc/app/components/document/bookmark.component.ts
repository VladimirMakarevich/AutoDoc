import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { BookmarkService } from "../../services/document/bookmark.service";
import { Router } from '@angular/router';
import { ActivatedRoute, Params } from '@angular/router';
import { Injectable } from '@angular/core';

import { Bookmark } from '../../Models/bookmark';

@Component(({
    selector: 'bookmark-component',
    template: './bookmark.component.html',
    providers: [BookmarkService]
}) as any)


@Injectable()
export class BookmarkComponent implements OnInit {

    bookmarks: Bookmark[];
    errorMessage: string;
    id: any;

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private location: Location,
        private bookmarkService: BookmarkService
    ) { }

    uploadNewValues(): void {
        this.bookmarkService.postData(this.bookmarks).subscribe(ans => {
            console.log(ans);
            this.router.navigate(['./download', this.id]);
        });
    }
        
    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
           this.id = params['id'];
            if (this.id != '') {
                this.bookmarkService.getData(this.id)
                    .subscribe(
                    bookmarks => this.bookmarks = bookmarks,
                    error => this.errorMessage = <any>error);
            }
        });
    }
    
}