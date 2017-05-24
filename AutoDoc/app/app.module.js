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
const upload_component_1 = require("./components/document/upload.component");
const bookmark_component_1 = require("./components/document/bookmark.component");
const download_component_1 = require("./components/document/download.component");
const document_service_1 = require("./services/document/document.service");
const bookmark_service_1 = require("./services/document/bookmark.service");
const ng2_smart_table_1 = require("ng2-smart-table");
const appRoutes = [
    { path: '', redirectTo: '/upload', pathMatch: 'full' },
    { path: 'upload', component: upload_component_1.UploadComponent, pathMatch: 'full' },
    { path: 'bookmark/:id', component: bookmark_component_1.BookmarkComponent },
    { path: 'download/:id', component: download_component_1.DownloadComponent }
];
const appRoutingProviders = [];
let AppModule = class AppModule {
};
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            ng2_smart_table_1.Ng2SmartTableModule,
            ng2_smart_table_1.LocalDataSource,
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            http_1.HttpModule,
            router_1.RouterModule.forRoot(appRoutes)
        ],
        providers: [
            { provide: common_1.APP_BASE_HREF, useValue: '/' },
            document_service_1.DocumentService,
            bookmark_service_1.BookmarkService
        ],
        declarations: [
            app_component_1.AppComponent,
            upload_component_1.UploadComponent,
            download_component_1.DownloadComponent,
            bookmark_component_1.BookmarkComponent
        ],
        exports: [
            app_component_1.AppComponent,
            router_1.RouterModule
        ],
        bootstrap: [
            app_component_1.AppComponent
        ],
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map