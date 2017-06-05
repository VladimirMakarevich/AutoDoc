import { Component, OnInit, ViewChild, ViewChildren, OnDestroy, Input } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { DataService } from '../../../services/data.service';
import { Bookmark } from '../../../models/bookmarks/bookmark.type';
import { Document } from '../../../models/document/document.type';
import { Router } from '@angular/router';

@Component({
    selector: 'document',
    templateUrl: 'app/components/document/upload/document.component.html',
})

export class DocumentComponent implements OnInit, OnDestroy {
    document: Document;

    constructor(private documentService: DocumentService, private dataservice: DataService,
        private router: Router) {
    }

    ngOnInit() {
    }

    ngOnDestroy() {
        this.dataservice.setDocument(this.document);
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
        }
    }
}