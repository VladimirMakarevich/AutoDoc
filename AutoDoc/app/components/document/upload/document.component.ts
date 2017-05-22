import { Component, OnInit, ViewChild, ViewChildren, OnDestroy, Input } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { DataService } from '../../../services/data.service';
import { Bookmark } from '../../../models/bookmarks/bookmark.type';
import { Document } from '../../../models/document/document.type';
import { Hero } from '../../../models/hero.type';
import { Router } from '@angular/router';

@Component({
    selector: 'document',
    templateUrl: 'app/components/document/upload/document.component.html',
    providers: [DocumentService, DataService]
})

export class DocumentComponent implements OnInit, OnDestroy {
    //@Input() document: Document;
    document: Document;

    hero: Hero = {
        name: "Utpal Kumar Das"
    }; 

    constructor(private documentService: DocumentService, public dataservice: DataService,
        private router: Router) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.dataservice.document = this.document;
        this.dataservice.hero = this.hero; 
    }

    navigateToBookmarks(document: Document) {
        document = this.document;
        this.router.navigate(['bookmarks']);
    }

    @ViewChild('fileInput') fileInput: any;

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .upload(fileToUpload)
                .then(document => {
                    console.log(document.bookmarks as Bookmark[]);
                    console.log(document.id);
                    this.document = document;
                    this.navigateToBookmarks(document);
                });
            //.subscribe(response => {
            //    console.log(response);
            //    this.document = response;
            //    this.navigateToBookmarks()
            //});
        }
    }
}