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
let ButtonRenderComponent = class ButtonRenderComponent {
    constructor() { }
    ngOnInit() {
        this.renderValue = this.value;
    }
    example() {
        alert(this.renderValue);
    }
};
__decorate([
    core_1.Input(),
    __metadata("design:type", Object)
], ButtonRenderComponent.prototype, "value", void 0);
ButtonRenderComponent = __decorate([
    core_1.Component({
        template: `
    <button (click)="example()">Click me</button>
  `,
    }),
    __metadata("design:paramtypes", [])
], ButtonRenderComponent);
exports.ButtonRenderComponent = ButtonRenderComponent;
//# sourceMappingURL=button.render.component.js.map