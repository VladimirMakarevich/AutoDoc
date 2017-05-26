"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
const http_2 = require("@angular/http");
require("rxjs/Rx");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
const Observable_1 = require("rxjs/Observable");
let BookmarkService = class BookmarkService {
    constructor(http) {
        this.http = http;
        this.bookmarkUrlGet = 'http://localhost:50347/api/Bookmark/GetBookmarks?id=';
        this.bookmarkUrlPost = 'http://localhost:50347/api/Bookmark/PostBookmarks';
    }
    postData(bookmarks) {
        let headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        headers.append('Accept', 'application/json; charset=utf-8');
        headers.append('Access-Control-Allow-Methods', 'POST, GET, DELETE, PUT');
        headers.append('Access-Control-Allow-Headers', "X-Requested-With, Content-Type, Origin, Authorization, Accept, Client-Security-Token, Accept-Encoding");
        let options = new http_2.RequestOptions({ method: 'POST', headers: headers });
        let body = JSON.stringify(bookmarks);
        return this.http.post(this.bookmarkUrlPost, body, options)
            .map(this.extractData)
            .catch(this.handleError);
    }
    getData(id) {
        return this.http.get(this.bookmarkUrlGet + id)
            .map((res) => {
            this.bookmarks = res.json();
            return this.bookmarks;
        })
            .catch(this.handleError);
    }
    extractData(res) {
        let body = res.json();
        return body;
    }
    handleError(error) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg;
        if (error instanceof http_1.Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return Observable_1.Observable.throw(errMsg);
    }
};
BookmarkService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], BookmarkService);
exports.BookmarkService = BookmarkService;
//# sourceMappingURL=bookmark.service.js.map