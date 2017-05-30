"use strict";
const ng2_smart_table_1 = require("ng2-smart-table");
/*export interface Bookmark {
    id: string;
    name: string;
    messagetable: Table;
    messagetext: string;
    type: number;
}*/
/*export class Table {
    constructor() {
        this.headers = new Array<string>();
        this.data = new Array<Array<string>>();
    }

    headers: string[];
    data: string[][];
}*/
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
class Header {
}
exports.Header = Header;
//# sourceMappingURL=bookmark.js.map