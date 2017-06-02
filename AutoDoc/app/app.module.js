"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
const core_1 = require("@angular/core");
const platform_browser_1 = require("@angular/platform-browser");
const forms_1 = require("@angular/forms");
const common_1 = require("@angular/common");
const router_1 = require("@angular/router");
const http_1 = require("@angular/http");
const app_component_1 = require("./components/app.component");
const document_component_1 = require("./components/document/upload/document.component");
const downdocument_component_1 = require("./components/document/download/downdocument.component");
const bookmarks_component_1 = require("./components/bookmarks/bookmarks.component");
const error_component_1 = require("./components/error/error.component");
const ng2_smart_table_1 = require("ng2-smart-table");
const document_service_1 = require("./services/document.service");
const bookmark_service_1 = require("./services/bookmark.service");
const data_service_1 = require("./services/data.service");
const routes = [
    { path: '', component: app_component_1.AppComponent },
    { path: 'document', component: document_component_1.DocumentComponent },
    { path: 'bookmarks', component: bookmarks_component_1.BookmarkComponent },
    { path: 'downdocument/:id', component: downdocument_component_1.DownDocumentComponent },
    { path: '**', component: error_component_1.ErrorComponent }
];
const appRoutingProviders = [];
let AppModule = class AppModule {
};
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, http_1.HttpModule, router_1.RouterModule.forRoot(routes), ng2_smart_table_1.Ng2SmartTableModule, forms_1.ReactiveFormsModule],
        providers: [{ provide: common_1.APP_BASE_HREF, useValue: '/' }, data_service_1.DataService, bookmark_service_1.BookmarkService, document_service_1.DocumentService],
        declarations: [app_component_1.AppComponent,
            document_component_1.DocumentComponent,
            downdocument_component_1.DownDocumentComponent,
            error_component_1.ErrorComponent,
            bookmarks_component_1.BookmarkComponent],
        exports: [app_component_1.AppComponent, router_1.RouterModule],
        bootstrap: [app_component_1.AppComponent],
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map