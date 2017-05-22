import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { RequestOptions, Request, RequestMethod } from '@angular/http'

import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/observable/throw';
import 'rxjs/Rx';

@Injectable()
export class DocumentService {

    constructor(private http: Http) {

    }

    upload(fileToUpload: any) {

        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http
            .post("http://localhost:50348/api/Document/UploadFile", input);
    }

    download(fileToDownload: any) {

        let body = JSON.stringify(fileToDownload);

        return this.http
            .get("http://localhost:50348/api/Document/DownloadDocument", body);
    }
}