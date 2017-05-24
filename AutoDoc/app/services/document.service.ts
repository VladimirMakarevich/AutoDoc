import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { Observable } from 'rxjs';
import { RequestOptions, Request, RequestMethod, ResponseContentType } from '@angular/http';

import { Bookmark } from '../models/bookmarks/bookmark.type';
import { Document } from '../models/document/document.type';
import { File } from '../models/document/file.type';

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
    private file: File;

    constructor(private http: Http) {
    }

    upload(fileToUpload: any) {

        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http
            .post("http://localhost:50348/api/Document/UploadFile", input)
                .map(response => response.json() as Document)
                .toPromise();
    }

    download(id: number): Observable<File> {

        return this.http
            .get("http://localhost:50348/api/Document/DownloadDocument/" + id, { responseType: ResponseContentType.Blob })
            .map((res: Response) => {

                var headerSection = res.headers.get('Content-Type');
                var headerFileName = headerSection.split(';')[1];
                var fileName = headerFileName.replace(/"/g, '');

                this.file = {
                    fileContents: res.blob(),
                    fileDownloadName: fileName
                };

                return this.file;
            });

    }
}