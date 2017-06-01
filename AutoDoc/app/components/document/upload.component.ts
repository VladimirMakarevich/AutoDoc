import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from "../../services/document/document.service";
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Component(({
    selector: 'upload-component',
    templateUrl: 'app/components/document/upload.component.html',
    providers: [DocumentService]
}) as any)


@Injectable()
export class UploadComponent {

    constructor(private documentService: DocumentService, private router: Router) { }

    @ViewChild('fileInput') fileInput: any;
    @ViewChild('fileNameInput') textInput: any;

    public fileName: string;

    fileChangeEvent(fileInput: any) {
        if (fileInput.target.files && fileInput.target.files[0]) {
            this.fileName = fileInput.target.files[0].name;
        }
    }

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];

            this.documentService.uploadFile(fileToUpload).subscribe((id: string) => {
                if (id != null) {
                    this.router.navigate(['/bookmark', id]);
                }
            });
        }
    }
}