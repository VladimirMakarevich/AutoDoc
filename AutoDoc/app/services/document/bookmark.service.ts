import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Bookmark } from '../../Models/bookmark';

@Injectable()
export class BookmarkService {

    constructor(private http: Http) { }

    private bookmarkUrlGet = 'http://localhost:50348/api/Bookmark/GetBookmarks?id='; 
    private bookmarkUrlPost = 'http://localhost:50348/api/Bookmark/PostBookmarks'; 

    postData(bookmarks: Bookmark[]): any {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.bookmarkUrlPost, { bookmarks }, options)
            .map(this.extractData)
            .catch(this.handleError);
    }

    getData(id: string): Observable<Bookmark[]> {
        return this.http.get(this.bookmarkUrlGet + id)
                        .map(this.extractData)
                        .catch(this.handleError);
    }

    private extractData(res: Response) {
        let body = res.json();
        return body.data || {};
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