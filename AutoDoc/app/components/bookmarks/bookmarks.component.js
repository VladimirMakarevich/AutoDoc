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
const table_type_1 = require("../../models/table/table.type");
const router_1 = require("@angular/router");
let BookmarkComponent = class BookmarkComponent {
    constructor(bookmarkService, dataservice, router) {
        this.bookmarkService = bookmarkService;
        this.dataservice = dataservice;
        this.router = router;
        this.inputOptions = Array();
    }
    example() {
        alert(this.renderValue);
    }
    ngOnInit() {
        this.document = this.dataservice.getDocument();
        let bufIterator = new Array();
        for (var i = 0; i < this.document.bookmarks.length; i++) {
            if (this.document.bookmarks[i].type == 2) {
                let buf = new table_type_1.Table();
                buf.settings = this.document.bookmarks[i].message.settings;
                buf.data.load(this.document.bookmarks[i].message.data);
                this.document.bookmarks[i].message = buf;
            }
        }
        this.inputOptions = Array();
        this.inputOptions.push(new Option(1, 'Text'));
        this.inputOptions.push(new Option(2, 'Table'));
        this.renderValue = this.value;
    }
    ngOnDestroy() {
    }
    changeBookmarkType(bookmark) {
        if (bookmark.type == 2) {
            bookmark.message = new table_type_1.Table();
        }
        if (bookmark.type == 1) {
            bookmark.message;
        }
    }
    addColumn(bookmark) {
        let set = bookmark.message.settings;
        set.columns[this.newColumn] = { title: this.newColumn, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, set);
    }
    uploadBookmarks(id) {
        let data = this.getTable(this.document);
        this.bookmarkService.uploadBookmarks(data)
            .then(id => {
            this.id = id;
            this.router.navigate(['/downdocument', id]);
        });
    }
    getTable(obj) {
        for (var i = 0; i < obj.bookmarks.length; i++) {
            if (obj.bookmarks[i].type == "2") {
                let dataTable = obj.bookmarks[i].message.data.data;
                obj.bookmarks[i].message.data = dataTable;
            }
        }
        return obj;
    }
};
__decorate([
    core_1.Input(),
    __metadata("design:type", Object)
], BookmarkComponent.prototype, "value", void 0);
BookmarkComponent = __decorate([
    core_1.Component({
        selector: 'bookmarks',
        templateUrl: 'app/components/bookmarks/bookmarks.component.html',
    }),
    __metadata("design:paramtypes", [bookmark_service_1.BookmarkService, data_service_1.DataService,
        router_1.Router])
], BookmarkComponent);
exports.BookmarkComponent = BookmarkComponent;
class Option {
    constructor(id, name) {
        this.id = id;
        this.name = name;
    }
}
exports.Option = Option;
//# sourceMappingURL=bookmarks.component.js.map