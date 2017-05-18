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

    constructor(private http: Http) { }

    private documentUrlGet = 'http://localhost:50348/api/Document/GetBookmarks?id=';
    private documentUrlPost = 'http://localhost:50348/api/Document/UploadFiles'; 

    downloadFile(id: string): Observable<File> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'MyApp-Application': 'AppName', 'Accept': 'application/pdf' });
        let options = new RequestOptions({ headers: headers, responseType: ResponseContentType.Blob });

        return this.http.get(this.documentUrlGet + id)
            .map(this.extractContent)
            .catch(this.handleError);
    }

    uploadFile(fileToUpload: any) {

        let input = new FormData();
        input.append("file", fileToUpload);

        return this.http
            .post(this.documentUrlPost, input);
    }

    private extractContent(res: Response) {
        let blob: Blob = res.blob();
        window['saveAs'](blob, 'test.docx');
    }

    private handleError(error: Response | any) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg: string;
        if (error instanceof Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable.throw(errMsg);
    }
}