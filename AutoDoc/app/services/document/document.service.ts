import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { Response, Headers } from '@angular/http';
import { RequestOptions, Request, RequestMethod, ResponseContentType } from '@angular/http';
import { Document } from '../../Models/document';
import { File } from '../../Models/file';
import "rxjs/Rx";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/toPromise';
import 'rxjs/add/observable/throw';
import { Observable } from 'rxjs';

@Injectable()
export class DocumentService {

    constructor(private http: Http) { }

    private documentUrlGet = 'http://localhost:50347/api/Document/DownloadFiles?id=';
    private documentUrlPost = 'http://localhost:50347/api/Document/UploadFiles'; 

    private doc: Document;
    private file: File;

    downloadFile(id: string): Observable<File> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'MyApp-Application': 'AppName', 'Accept': 'application/pdf' });
        let options = new RequestOptions({ headers: headers });

        return this.http.get(this.documentUrlGet + id, { responseType: ResponseContentType.Blob })
            .map((res: Response) => {
                
                var headerSection = res.headers.get('Content-Type');
                var headerFileName = headerSection.split(';')[1];
                var fileName = headerFileName.replace(/"/g, '');

                //console.log(headerSection);
                //console.log(headerFileName);
                //console.log(fileName);

                this.file = {
                    fileContents: res.blob(),
                    fileDownloadName: fileName
                };
               
                return this.file;
            })
            .catch(this.handleError);
    }

    uploadFile(fileToUpload: any): Observable<string> {

        let input = new FormData();
        input.append("file", fileToUpload);
        

        return this.http
            .post(this.documentUrlPost, input)
            .map((res: Response) => {
                this.doc = <Document>res.json();  
                return this.doc.id;
            })
            .catch(this.handleError);
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