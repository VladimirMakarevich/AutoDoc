import { Injectable } from "@angular/core";

/*export interface Bookmark {
    id: string;
    name: string;
    messagetable: Table;
    messagetext: string;
    type: number;
}*/

export class Table {
    headers: any;
    data: any;
}

export interface Bookmark {
    id: string;
    name: string;
    message: any;
    type: number;
}