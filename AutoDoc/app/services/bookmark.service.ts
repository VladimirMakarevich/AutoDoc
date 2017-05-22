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

    upload(obj: any) {

        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append('Accept', 'application/json; charset=utf-8');
        headers.append('Access-Control-Allow-Methods', 'POST, GET, DELETE, PUT');
        headers.append('Access-Control-Allow-Headers', "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding");

        let options = new RequestOptions({ method: 'POST', headers: headers });
        let body = JSON.stringify(obj);
        let url = "http://localhost:50348/api/Document/ReplaceBookmarks";

        return this.http.post(url, body, options);
    }
}