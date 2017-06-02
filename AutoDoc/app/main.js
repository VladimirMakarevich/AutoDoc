"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
const app_module_1 = require("./app.module");
require("rxjs/add/operator/map");
require("rxjs/add/operator/mergeMap");
require("rxjs/add/observable/of");
require("rxjs/add/operator/do");
const platform = platform_browser_dynamic_1.platformBrowserDynamic();
platform.bootstrapModule(app_module_1.AppModule);
//# sourceMappingURL=main.js.map