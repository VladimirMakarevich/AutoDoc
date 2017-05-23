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
const bookmark_type_1 = require("../../models/bookmarks/bookmark.type");
const document_type_1 = require("../../models/document/document.type");
let BookmarkComponent = class BookmarkComponent {
    constructor(bookmarkService, dataservice) {
        this.bookmarkService = bookmarkService;
        this.dataservice = dataservice;
    }
    ngOnInit() {
        this.document = this.dataservice.document;
        this.hero = this.dataservice.hero;
    }
    ngOnDestroy() {
    }
    uploadBookmarks() {
    }
};
BookmarkComponent = __decorate([
    core_1.Component({
        selector: 'bookmarks',
        //templateUrl: 'app/components/bookmarks/bookmarks.component.html',
        template: `<div>
<h3>Page One </h3>
  <input [(ngModel)]="hero.name" type="text" />
  <br />
	<h2>Your Bookmarks</h2>
        <ul>
        <li *ngFor="let bookmark of document.bookmarks">
            <span>{{bookmark.id}}</span> {{bookmark.name}}
            <input [(ngModel)]="bookmark.message" placeholder="New Value" />
        </li>
    </ul>
	<button (click)="uploadBookmarks(document.id)">Set</button>
</div>
  `,
        providers: [bookmark_service_1.BookmarkService, data_service_1.DataService, bookmark_type_1.Bookmark, document_type_1.Document, DocumentComponent]
    }),
    __metadata("design:paramtypes", [bookmark_service_1.BookmarkService, data_service_1.DataService])
], BookmarkComponent);
exports.BookmarkComponent = BookmarkComponent;
//# sourceMappingURL=bookmarks.component.js.map