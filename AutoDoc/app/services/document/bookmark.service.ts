import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions, ResponseContentType } from '@angular/http';
import "rxjs/Rx";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { Bookmark } from '../../Models/bookmark';
import { File } from '../../Models/file';

@Injectable()
export class BookmarkService {

    constructor(private http: Http) { }

    private bookmarkUrlGet = 'http://localhost:50347/api/Bookmark/GetBookmarks?id='; 
    private bookmarkUrlPost = 'http://localhost:50347/api/Bookmark/PostBookmarks'; 
    private bookmarkPicUrlPost = 'http://localhost:50347/api/Bookmark/PostBookmarkPictures'; 
    private bookmarkPicUrlGet = 'http://localhost:50347/api/Bookmark/GetBookmarkPictures?name='; 

    private bookmarks: Bookmark[];

    postData(bookmarks: Bookmark[]): any {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Accept', 'application/json; charset=utf-8');
        headers.append('Access-Control-Allow-Methods', 'POST, GET, DELETE, PUT');
        headers.append('Access-Control-Allow-Headers', "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding");
        let options = new RequestOptions({ method: 'POST', headers: headers });

        
        let body = JSON.stringify(bookmarks);
        return this.http.post(this.bookmarkUrlPost, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getData(id: string): Observable<Bookmark[]> {
        return this.http.get(this.bookmarkUrlGet + id)
            .map((res: Response) => {
                this.bookmarks = <Bookmark[]>res.json();
                return this.bookmarks;
            })
            .catch(this.handleError)
    }

    postImageFile(fileToUpload: any): Promise<string> {
        let input = new FormData();
        input.append("file", fileToUpload);


        return this.http
            .post(this.bookmarkPicUrlPost, input)
            .toPromise()
            .then((res: Response) => {
                return res.text();
            })
            .catch(this.handleError);
    }

    getImageFile(name: string): Promise<File> {
        let headers = new Headers({ 'Content-Type': 'application/json', 'MyApp-Application': 'AppName', 'Accept': 'application/jpeg' });
        let options = new RequestOptions({ headers: headers });

        return this.http.get(this.bookmarkPicUrlGet + name, { responseType: ResponseContentType.Blob })
            .toPromise()
            .then((res: Response) => {

                var headerSection = res.headers.get('Content-Type');
                var headerFileName = headerSection.split(';')[1];
                var fileName = headerFileName.replace(/"/g, '');

                //console.log(headerSection);
                //console.log(headerFileName);
                //console.log(fileName);

                let file = {
                    fileContents: res.blob(),
                    fileDownloadName: fileName
                };

                return file;
            })
            .catch(this.handleError);
    }
 
    private extractData(res: Response): any {
        let body = res.json();
        return body;
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