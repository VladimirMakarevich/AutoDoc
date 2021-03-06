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
require("rxjs/add/operator/catch");
require("rxjs/add/operator/debounceTime");
require("rxjs/add/operator/distinctUntilChanged");
require("rxjs/add/operator/map");
require("rxjs/add/operator/switchMap");
require("rxjs/add/operator/toPromise");
require("rxjs/add/observable/throw");
require("rxjs/Rx");
let DocumentService = class DocumentService {
    constructor(http) {
        this.http = http;
    }
    upload(fileToUpload) {
        let input = new FormData();
        input.append("file", fileToUpload);
        return this.http
            .post("http://localhost:50348/api/Document/UploadFile", input)
            .map(response => response.json())
            .toPromise();
    }
    download(id) {
        return this.http
            .get("http://localhost:50348/api/Document/DownloadDocument/" + id, { responseType: http_2.ResponseContentType.Blob })
            .map((res) => {
            var headerSection = res.headers.get('Content-Type');
            var headerFileName = headerSection.split(';')[1];
            var fileName = headerFileName.replace(/"/g, '');
            this.file = {
                fileContents: res.blob(),
                fileDownloadName: fileName
            };
            return this.file;
        });
    }
};
DocumentService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], DocumentService);
exports.DocumentService = DocumentService;
//# sourceMappingURL=document.service.js.map