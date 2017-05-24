import { Component, OnInit, ViewChild, ViewChildren, OnDestroy, Input } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { Document } from '../../../models/document/document.type';
import { File } from '../../../models/document/file.type';

import { Router, ActivatedRoute } from '@angular/router';
import { saveAs } from 'file-saver';

@Component({
    selector: 'downdocument',
    templateUrl: 'app/components/document/download/downdocument.component.html',
})

export class DownDocumentComponent {
    private documentId: any;
    private file: File;
        

    constructor(private router: Router, private documentService: DocumentService, route: ActivatedRoute) {
        this.documentId = route.snapshot.params['id']; 
    }

    downloadDocument(id: number): void {
        this.documentService
            .download(id)
            .subscribe((file: File) => {
            var link = document.createElement('a');
            link.href = window.URL.createObjectURL(file.fileContents);
            link.download = "edit_" + file.fileDownloadName;
            link.click();
        });
          
    }
}