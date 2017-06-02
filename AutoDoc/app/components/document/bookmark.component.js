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
const bookmark_service_1 = require("../../services/document/bookmark.service");
const router_1 = require("@angular/router");
const router_2 = require("@angular/router");
const core_2 = require("@angular/core");
const bookmark_1 = require("../../Models/bookmark");
const ng_editable_table_1 = require("ng-editable-table");
const file_1 = require("../../Models/file");
let BookmarkComponent = class BookmarkComponent {
    constructor(routeActivated, router, bookmarkService, tableService, ref, ngZone) {
        this.routeActivated = routeActivated;
        this.router = router;
        this.bookmarkService = bookmarkService;
        this.tableService = tableService;
        this.ref = ref;
        this.ngZone = ngZone;
        this.inputOptions = Array();
    }
    changeBookmarkType(bookmark) {
        if (bookmark.type == 2)
            bookmark.message = new bookmark_1.Table();
        if (bookmark.type == 1)
            bookmark.message = '';
        if (bookmark.type == 3)
            bookmark.message = new file_1.File();
    }
    addNewHeader(bookmark) {
        let buf = bookmark.message.settings;
        buf.columns[this.newHeaderName] = { title: this.newHeaderName, sort: false, filter: false };
        bookmark.message.settings = Object.assign({}, buf);
    }
    fileSelected(fileInput, bookmark) {
        if (fileInput.target.files && fileInput.target.files[0]) {
            bookmark.message.fileContents = fileInput.target.files[0];
            bookmark.message.fileDownloadName = fileInput.target.files[0].name;
        }
    }
    uploadNewValues() {
        console.log(this.bookmarks);
        console.log(this.bookmarks[0].message);
        this.bookmarkService.postData(this.bookmarks).subscribe((ans) => {
            this.router.navigate(['./download', this.id]);
        });
    }
    prepareNewValues() {
        //let bufIterator = new Array<number>();
        //let allPromiseOperations: number = this.bookmarks.filter(el => el.type == 3).length;
        //let donePromiseOperations: number = 0;
        for (let i = 0; i < this.bookmarks.length; i++) {
            if (this.bookmarks[i].type == 2) {
                let dataTable = this.bookmarks[i].message.data.data;
                this.bookmarks[i].message.data = dataTable;
            }
            /*if (this.bookmarks[i].type == 3) {
                bufIterator.push(i);

                this.bookmarkService.postImageFile(this.bookmarks[i].message.fileContents).then((name: string) => {
                    if (name != null) {

                        let buf = new Bookmark();

                        buf.id = this.bookmarks[bufIterator[0]].id;
                        buf.name = this.bookmarks[bufIterator[0]].name;
                        buf.type = 3;
                        buf.message = name;

                        //this.bookmarks[bufIterator[0]].message = null;
                        //this.bookmarks[bufIterator[0]].message = new String(name);
                        //this.bookmarks[bufIterator[0]].message = name;

                        this.bookmarks[bufIterator[0]] = null;
                        this.bookmarks[bufIterator[0]] = buf;

                        bufIterator.splice(0, 1);
                    }

                    donePromiseOperations++;
                    if (donePromiseOperations == allPromiseOperations && i >= this.bookmarks.length) this.uploadNewValues();

                });
            }*/
        }
        //if (donePromiseOperations == allPromiseOperations && i >= this.bookmarks.length) this.uploadNewValues();
        this.uploadNewValues();
    }
    ngOnInit() {
        this.routeActivated.params.subscribe((params) => {
            this.id = params['id'];
            if (this.id != '') {
                this.bookmarkService.getData(this.id).subscribe((bookmarks) => {
                    let bufIterator = new Array();
                    for (var i = 0; i < bookmarks.length; i++) {
                        if (bookmarks[i].type == 2) {
                            let buf = new bookmark_1.Table();
                            buf.settings = bookmarks[i].message.settings;
                            buf.data.load(bookmarks[i].message.data);
                            bookmarks[i].message = buf;
                        }
                        /*if (bookmarks[i].type == 3) {
                            bufIterator.push(i);
                            this.bookmarkService.getImageFile(bookmarks[i].message).then((file: File) => {
                                if (file != null) {
                                    bookmarks[bufIterator[0]].message = new File();
                                    bookmarks[bufIterator[0]].message = file;
                                    bufIterator.splice(0, 1);
                                }
                            });
                        }*/
                    }
                    this.bookmarks = bookmarks;
                });
            }
        });
        this.inputOptions = Array();
        this.inputOptions.push(new Option(1, 'Text'));
        this.inputOptions.push(new Option(2, 'Table'));
        //this.inputOptions.push(new Option(3, 'Image'));
    }
};
BookmarkComponent = __decorate([
    core_1.Component(({
        selector: 'bookmark-component',
        templateUrl: 'app/components/document/bookmark.component.html',
        providers: [bookmark_service_1.BookmarkService]
    })),
    core_2.Injectable(),
    __metadata("design:paramtypes", [router_2.ActivatedRoute,
        router_1.Router,
        bookmark_service_1.BookmarkService,
        ng_editable_table_1.EditableTableService,
        core_2.ApplicationRef,
        core_2.NgZone])
], BookmarkComponent);
exports.BookmarkComponent = BookmarkComponent;
class Option {
    constructor(id, name) {
        this.id = id;
        this.name = name;
    }
}
exports.Option = Option;
//# sourceMappingURL=bookmark.component.js.map