import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import "rxjs/Rx";
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import { Observable } from 'rxjs/Observable';

import { Bookmark } from '../../Models/bookmark';

@Injectable()
export class BookmarkService {

    constructor(private http: Http) { }

    private bookmarkUrlGet = 'http://localhost:50347/api/Bookmark/GetBookmarks?id='; 
    private bookmarkUrlPost = 'http://localhost:50347/api/Bookmark/PostBookmarks'; 

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