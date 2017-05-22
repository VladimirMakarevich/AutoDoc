import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { Bookmark } from '../../../models/bookmarks/bookmark.type';
import { Document } from '../../../models/document/document.type';
import { Router } from '@angular/router';

@Component({
    selector: 'document',
    templateUrl: 'app/components/document/upload/document.component.html',
    providers: [DocumentService]
})

export class DocumentComponent {

    private document: any;

    constructor(private documentService: DocumentService,
        private router: Router) {
    }

    navigateToBookmarks() {
        //this.router.navigate(['bookmarks', this.document]);
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
                    console.log(document.bookmarks);
                    console.log(document.id);
                    this.document = document;
                    this.navigateToBookmarks()
                });
                //.subscribe(response => {
                //    console.log(response);
                //    this.document = response;
                //    this.navigateToBookmarks()
                //});
        }
    }
}