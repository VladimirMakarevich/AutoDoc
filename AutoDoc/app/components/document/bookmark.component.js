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
let BookmarkComponent = class BookmarkComponent {
    constructor(routeActivated, router, bookmarkService, tableService, ref, ngZone) {
        this.routeActivated = routeActivated;
        this.router = router;
        this.bookmarkService = bookmarkService;
        this.tableService = tableService;
        this.ref = ref;
        this.ngZone = ngZone;
        this.inputOptions = Array();
        this.mySettings = new bookmark_1.Settings();
    }
    changeBookmarkType(bookmark) {
        if (bookmark.type == 2) {
            bookmark.message = new bookmark_1.Table();
            //bookmark.message.headers = new Array<string>();
            //bookmark.message.data = new Array<Array<string>>();
            /*bookmark.message.headers = ['qqq', 'www', 'zzz'];
            bookmark.message.data = [
                ['qqq', 'www', 'zzz'],
                ['qqq', 'www', 'zzz'],
                ['qqq', 'www', 'zzz']
            ];*/
        }
        if (bookmark.type == 1)
            bookmark.message;
    }
    addNewHeader(bookmark) {
        /*this.ngZone.run(() => {
            //bookmark.message.headers.push(this.newHeaderName);
            //bookmark.message.data.push(new Array<string>());
        });*/
        this.mySettings.columns[this.newHeaderName] = { title: this.newHeaderName };
        bookmark.message.settings = Object.assign({}, this.mySettings);
        //this.ref.tick();
        //NgZone.run(() => this.currentUser.next(user));
        console.log(bookmark.message);
        //this.tableService.tableHeadersObjects.push(this.newHeaderName);
        //this.tableService.tableHeadersObjects.values.apply();
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.headers.push(this.newHeaderName);
        //this.bookmarks.filter(el => el.id == id)[0].messagetable.data
    }
    uploadNewValues() {
        console.log(this.bookmarks[0].message);
        this.bookmarkService.postData(this.bookmarks).subscribe((ans) => {
            //console.log(ans);
            this.router.navigate(['./download', this.id]);
        });
    }
    ngOnInit() {
        this.routeActivated.params.subscribe((params) => {
            this.id = params['id'];
            if (this.id != '') {
                this.bookmarkService.getData(this.id).subscribe((bookmarks) => {
                    this.bookmarks = bookmarks;
                });
            }
        });
        this.inputOptions = Array();
        this.inputOptions.push(new Option(1, 'Text'));
        this.inputOptions.push(new Option(2, 'Table'));
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