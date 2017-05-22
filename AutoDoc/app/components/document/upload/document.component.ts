import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from '../../../services/document.service';

@Component({
    selector: 'document',
    templateUrl: 'app/components/document/upload/document.component.html',
    providers: [DocumentService]
})

export class DocumentComponent {

    private bookmarks: any;

    constructor(private documentService: DocumentService) {
    }

    @ViewChild('fileInput') fileInput: any;

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .upload(fileToUpload)
                .subscribe(res => {
                    console.log(res); this.bookmarks = res;
                });
        }
    }
}