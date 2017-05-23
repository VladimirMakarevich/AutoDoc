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
const bookmark_service_1 = require("../../services/bookmark.service");
const data_service_1 = require("../../services/data.service");
//import { WebStorageModule, LocalStorageService } from 'angular2-local-storage';
const router_1 = require("@angular/router");
let BookmarkComponent = class BookmarkComponent {
    constructor(bookmarkService, dataservice, router) {
        this.bookmarkService = bookmarkService;
        this.dataservice = dataservice;
        this.router = router;
    }
    ngOnInit() {
        this.document = this.dataservice.getDocument();
    }
    ngOnDestroy() {
    }
    //navigateToDownload(id: number) {
    //    this.router.navigate(['/download-document'], id);
    //}
    uploadBookmarks(id) {
        this.bookmarkService.uploadBookmarks(this.document)
            .then(id => {
            this.id = id;
            this.router.navigate(['/downdocument', id]);
        });
        //    subscribe((id: string) => {
        //    if (id != null) {
        //        this.router.navigate(['/bookmark', id]);
        //    }
        //});
    }
};
BookmarkComponent = __decorate([
    core_1.Component({
        selector: 'bookmarks',
        templateUrl: 'app/components/bookmarks/bookmarks.component.html',
    }),
    __metadata("design:paramtypes", [bookmark_service_1.BookmarkService, data_service_1.DataService, router_1.Router])
], BookmarkComponent);
exports.BookmarkComponent = BookmarkComponent;
//# sourceMappingURL=bookmarks.component.js.map