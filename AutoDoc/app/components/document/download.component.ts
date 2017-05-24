import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import { DocumentService } from "../../services/document/document.service";
import { ActivatedRoute, Params } from '@angular/router';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { Document } from '../../Models/document';
import { File } from '../../Models/file';

@Component(({
    selector: 'download-component',
    templateUrl: 'app/components/document/download.component.html',
    providers: [DocumentService]
}) as any)


@Injectable()
export class DownloadComponent implements OnInit {

    id: any;

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private documentService: DocumentService
    ) { }

    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
            this.id = params['id'];
        });
    }

    getFile(): void {
        this.documentService.downloadFile(this.id)
            .subscribe((file: File) => {
                var link = document.createElement('a');
                link.href = window.URL.createObjectURL(file.fileContents);
                link.download = "edit_" + file.fileDownloadName;
                link.click();
            });
    }
}