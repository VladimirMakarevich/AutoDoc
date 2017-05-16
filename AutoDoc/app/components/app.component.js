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
const document_service_1 = require("../../app/services/document.service");
let AppComponent = class AppComponent {
    constructor(documentService) {
        this.documentService = documentService;
    }
    addFile() {
        let fi = this.fileInput.nativeElement;
        if (fi.files && fi.files[0]) {
            let fileToUpload = fi.files[0];
            this.documentService
                .upload(fileToUpload)
                .subscribe(res => {
                console.log(res);
            });
        }
    }
};
__decorate([
    core_1.ViewChild('fileInput'),
    __metadata("design:type", Object)
], AppComponent.prototype, "fileInput", void 0);
AppComponent = __decorate([
    core_1.Component({
        selector: 'autodoc-app',
        template: `<div>
                    <h1>AutDoc App</h1>
                    <div class="form-group">
                        <div class="col-md-10">
                            <p>Upload one or more files using this form:</p>
                            <input #fileInput type="file" name="files" multiple />
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-md-10">
                            <input type="submit" value="Upload" (click)="addFile()"/>
                        </div>
                    </div>
                </div>`,
        providers: [document_service_1.DocumentService]
    }),
    __metadata("design:paramtypes", [document_service_1.DocumentService])
], AppComponent);
exports.AppComponent = AppComponent;
//# sourceMappingURL=app.component.js.map