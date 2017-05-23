import { Component, OnInit, ViewChild, ViewChildren, OnDestroy, Input } from '@angular/core';
import { DocumentService } from '../../../services/document.service';
import { Document } from '../../../models/document/document.type';
import { Router } from '@angular/router';

@Component({
    selector: 'downdocument',
    templateUrl: 'app/components/document/download/downdocument.component.html',
})

export class DownDocumentComponent {

    constructor(private router: Router, private documentService: DocumentService, ) {
    }

    downloadDocument(id: number): void {
        this.documentService
            .download(id);
            });
    }
}
