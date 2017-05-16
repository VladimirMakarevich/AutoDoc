import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from '../../app/services/document.service';

@Component({
    selector: 'autodoc-app',
    template: `<div>
                    <h1>AutDoc App</h1>
                    <div class="form-group">
                        <div class="col-md-10">
                            <p>Upload one or more files using this form:</p>
                            <input #fileInput type="file" name="files" multiple />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-10">
                            <input type="submit" value="Upload" (click)="addFile()"/>
                        </div>
                    </div>
                </div>`,
    providers: [DocumentService]
})

export class AppComponent {

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
                    console.log(res);
                });
        }
    }
}