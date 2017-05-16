import { Component, OnInit, ViewChild} from '@angular/core';

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
                            <input value="Upload" (click)="addFile()"/>
                        </div>
                    </div>
                </div>`,
})

export class AppComponent {

    @ViewChild("fileInput") fileInput;

    addFile(): void {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.uploadService
                .upload(fileToUpload)
                .subscribe(res => {
                    console.log(res);
                });
        }
    }
}