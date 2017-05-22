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
const document_service_1 = require("../../../services/document.service");
const router_1 = require("@angular/router");
let DocumentComponent = class DocumentComponent {
    constructor(documentService, router) {
        this.documentService = documentService;
        this.router = router;
    }
    navigateToBookmarks() {
        //this.router.navigate(['bookmarks', this.document]);
        this.router.navigate(['bookmarks']);
    }
    addFile() {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .upload(fileToUpload)
                .then(document => {
                console.log(document.bookmarks);
                console.log(document.id);
                this.document = document;
                this.navigateToBookmarks();
            });
            //.subscribe(response => {
            //    console.log(response);
            //    this.document = response;
            //    this.navigateToBookmarks()
            //});
        }
    }
};
__decorate([
    core_1.ViewChild('fileInput'),
    __metadata("design:type", Object)
], DocumentComponent.prototype, "fileInput", void 0);
DocumentComponent = __decorate([
    core_1.Component({
        selector: 'document',
        templateUrl: 'app/components/document/upload/document.component.html',
        providers: [document_service_1.DocumentService]
    }),
    __metadata("design:paramtypes", [document_service_1.DocumentService,
        router_1.Router])
], DocumentComponent);
exports.DocumentComponent = DocumentComponent;
//# sourceMappingURL=document.component.js.map