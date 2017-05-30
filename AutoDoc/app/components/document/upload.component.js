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
const document_service_1 = require("../../services/document/document.service");
const router_1 = require("@angular/router");
const core_2 = require("@angular/core");
let UploadComponent = class UploadComponent {
    constructor(documentService, router) {
        this.documentService = documentService;
        this.router = router;
    }
    addFile() {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService.uploadFile(fileToUpload).subscribe((id) => {
                if (id != null) {
                    this.router.navigate(['/bookmark', id]);
                }
            });
        }
    }
};
__decorate([
    core_1.ViewChild('fileInput'),
    __metadata("design:type", Object)
], UploadComponent.prototype, "fileInput", void 0);
UploadComponent = __decorate([
    core_1.Component(({
        selector: 'upload-component',
        templateUrl: 'app/components/document/upload.component.html',
        providers: [document_service_1.DocumentService]
    })),
    core_2.Injectable(),
    __metadata("design:paramtypes", [document_service_1.DocumentService, router_1.Router])
], UploadComponent);
exports.UploadComponent = UploadComponent;
//# sourceMappingURL=upload.component.js.map