import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { Router } from '@angular/router';

@Component({
    selector: 'upload-document',
    templateUrl: 'app/components/document/upload/document.component.html',
    providers: [DocumentService]
})

export class DocumentComponent {

    private bookmarks: any;

    constructor(private documentService: DocumentService,
        private router: Router) {
    }

    navigateToBookmarks() {
        this.router.navigate(['bookmarks', this.bookmarks]);
    }


    @ViewChild('fileInput') fileInput: any;

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .upload(fileToUpload)
                .subscribe(response => {
                    console.log(response);
                    this.bookmarks = response;
                    this.navigateToBookmarks()
                });
        }
    }
}