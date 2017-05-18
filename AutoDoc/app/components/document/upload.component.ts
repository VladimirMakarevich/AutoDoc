import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from "../../services/document/document.service";
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Component(({
    selector: 'upload-component',
    template: './upload.component.html',
    providers: [DocumentService]
}) as any)


@Injectable()
export class UploadComponent {

    constructor(private documentService: DocumentService, private router: Router) { }

    @ViewChild('fileInput') fileInput: any;

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .uploadFile(fileToUpload)
                .subscribe(id => {
                    console.log(id);
                    this.router.navigate(['./bookmark', id]);
                });
        }
    }
}