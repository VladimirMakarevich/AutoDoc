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
const document_service_1 = require("../../services/document/document.service");
const router_1 = require("@angular/router");
const router_2 = require("@angular/router");
const core_2 = require("@angular/core");
let DownloadComponent = class DownloadComponent {
    constructor(routeActivated, router, location, documentService) {
        this.routeActivated = routeActivated;
        this.router = router;
        this.location = location;
        this.documentService = documentService;
    }
    ngOnInit() {
        this.routeActivated.params.subscribe((params) => {
            this.id = params['id'];
        });
    }
    getFile() {
        this.documentService.downloadFile(this.id);
    }
};
DownloadComponent = __decorate([
    core_1.Component(({
        selector: 'download-component',
        template: './download.component.html',
        providers: [document_service_1.DocumentService]
    })),
    core_2.Injectable(),
    __metadata("design:paramtypes", [router_1.ActivatedRoute,
        router_2.Router,
        Location,
        document_service_1.DocumentService])
], DownloadComponent);
exports.DownloadComponent = DownloadComponent;
//# sourceMappingURL=download.component.js.map