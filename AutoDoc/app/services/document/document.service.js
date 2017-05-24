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
require("rxjs/Rx");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/debounceTime");
require("rxjs/add/operator/distinctUntilChanged");
require("rxjs/add/operator/map");
require("rxjs/add/operator/switchMap");
require("rxjs/add/operator/toPromise");
require("rxjs/add/observable/throw");
const rxjs_1 = require("rxjs");
let DocumentService = class DocumentService {
    constructor(http) {
        this.http = http;
        this.documentUrlGet = 'http://localhost:50347/api/Document/DownloadFiles?id=';
        this.documentUrlPost = 'http://localhost:50347/api/Document/UploadFiles';
    }
    downloadFile(id) {
        let headers = new http_2.Headers({ 'Content-Type': 'application/json', 'MyApp-Application': 'AppName', 'Accept': 'application/pdf' });
        let options = new http_3.RequestOptions({ headers: headers });
        return this.http.get(this.documentUrlGet + id, { responseType: http_3.ResponseContentType.Blob })
            .map((res) => {
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
    uploadFile(fileToUpload) {
        let input = new FormData();
        input.append("file", fileToUpload);
        return this.http
            .post(this.documentUrlPost, input)
            .map((res) => {
            this.doc = res.json();
            return this.doc.id;
        })
            .catch(this.handleError);
    }
    handleError(error) {
        // In a real world app, you might use a remote logging infrastructure
        let errMsg;
        if (error instanceof http_2.Response) {
            const body = error.json() || '';
            const err = body.error || JSON.stringify(body);
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(errMsg);
        return rxjs_1.Observable.throw(errMsg);
    }
};
DocumentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], DocumentService);
exports.DocumentService = DocumentService;
//# sourceMappingURL=document.service.js.map