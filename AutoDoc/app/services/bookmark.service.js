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
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const http_1 = require("@angular/http");
const http_2 = require("@angular/http");
const http_3 = require("@angular/http");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/debounceTime");
require("rxjs/add/operator/distinctUntilChanged");
require("rxjs/add/operator/map");
require("rxjs/add/operator/switchMap");
require("rxjs/add/operator/toPromise");
require("rxjs/add/observable/throw");
require("rxjs/Rx");
let BookmarkService = class BookmarkService {
    constructor(http) {
        this.http = http;
    }
    uploadBookmarks(obj) {
        let headers = new http_2.Headers({ 'Content-Type': 'application/json' });
        let options = new http_3.RequestOptions({ method: 'POST', headers: headers });
        let body = JSON.stringify(obj);
        let url = "http://localhost:50348/api/Document/ReplaceBookmarks";
        return this.http.post(url, body, options)
            .map(response => response.json())
            .toPromise();
    }
};
BookmarkService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], BookmarkService);
exports.BookmarkService = BookmarkService;
//# sourceMappingURL=bookmark.service.js.map