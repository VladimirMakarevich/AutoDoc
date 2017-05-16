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
const app_component_1 = require("./app.component");
const product_component_1 = require("./component/product/product.component");
const home_component_1 = require("./component/home/home.component");
const not_found_component_1 = require("./component/error/not.found.component");
const registration_component_1 = require("./registration.component");
const login_component_1 = require("./component/login/login.component");
// определение маршрутов
const routes = [
    { path: 'home', component: home_component_1.HomeComponent },
    { path: '', component: product_component_1.ProductListComponent },
    { path: 'login', component: login_component_1.LoginComponent },
    { path: 'registration', component: registration_component_1.RegistrationComponent },
    { path: '**', component: not_found_component_1.NotFoundComponent }
];
const appRoutingProviders = [];
let AppModule = class AppModule {
};
AppModule = __decorate([
    core_1.NgModule({
        imports: [platform_browser_1.BrowserModule, forms_1.FormsModule, http_1.HttpModule, router_1.RouterModule.forRoot(routes)],
        providers: [{ provide: common_1.APP_BASE_HREF, useValue: '/' }],
        exports: [router_1.RouterModule],
        declarations: [app_component_1.AppComponent,
            home_component_1.HomeComponent,
            not_found_component_1.NotFoundComponent,
            product_component_1.ProductListComponent,
            registration_component_1.RegistrationComponent,
            login_component_1.LoginComponent],
        exports: [app_component_1.AppComponent],
        bootstrap: [app_component_1.AppComponent],
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map