import { Injectable } from "@angular/core";
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';

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

export class Table {
    constructor() {
        this.settings = new Settings();
        this.data = new LocalDataSource();
    }

    settings: Settings;
    data: any;
}

export class Settings {
    constructor() {
        this.columns = new Object;
        this.mode = 'inline';
    }
    mode: string;
    columns: any;
}

export class Header {
    title: string;
    filter: boolean;
    sort: boolean;
}

export interface Bookmark {
    id: string;
    name: string;
    message: any;
    type: number;
}