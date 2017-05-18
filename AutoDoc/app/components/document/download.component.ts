import { Component, OnInit, ViewChild, ViewChildren } from '@angular/core';
import {  DocumentService } from "../../services/document/document.service";
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Component(({
    selector: 'download-component',
    template: './download.component.html',
    providers: [DocumentService]
}) as any)


@Injectable()
export class DownloadComponent implements OnInit {

    id: any;

    constructor(
        private routeActivated: ActivatedRoute,
        private router: Router,
        private location: Location,
        private documentService: DocumentService
    ) { }

    ngOnInit() {
        this.routeActivated.params.subscribe((params: Params) => {
            this.id = params['id'];
        });
    }

    getFile(): void {
        this.documentService.downloadFile(this.id);
    }
}