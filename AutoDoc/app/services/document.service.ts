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
            .post("/api/Document/UploadFiles", input);
    }

    //postData(obj: id) {

    //    let headers = new Headers({
    //        'Content-Type': 'application/json'
    //    });

    //    headers.append('Accept', 'application/json; charset=utf-8');
    //    headers.append('Access-Control-Allow-Methods', 'POST, GET, DELETE, PUT');
    //    headers.append('Access-Control-Allow-Headers', "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding");
    //    console.log("headers1: value" + JSON.stringify(headers));

    //    let options = new RequestOptions({ method: 'POST', headers: headers });

    //    let url = "http://localhost:51377/api/PayPal/PaymentWithPaypal/";

    //    var currentUrl = "http://localhost:3000/";
    //    var data = new Object();
    //    data.ProductId = obj;
    //    data.Url = currentUrl;

    //    var PaymentPaypalJsonModel = JSON.stringify(data);

    //    return this.http.post(url, PaymentPaypalJsonModel, options)
    //        .subscribe(data => { window.open(JSON.parse(data._body) )
    //        });
    //    }
}