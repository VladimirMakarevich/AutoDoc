"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const ng2_smart_table_1 = require("ng2-smart-table");
class Table {
    constructor() {
        this.settings = new Settings();
        this.data = new ng2_smart_table_1.LocalDataSource();
    }
}
exports.Table = Table;
class Settings {
    constructor() {
        this.columns = new Object;
        this.mode = 'inline';
    }
}
exports.Settings = Settings;
//# sourceMappingURL=table.type.js.map