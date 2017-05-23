import { Injectable } from '@angular/core';
import { Bookmark } from '../models/bookmarks/bookmark.type';
import { Document } from '../models/document/document.type';

@Injectable()
export class DataService {
    private document: Document;

    public setDocument(data: any) {
        this.document = data;
    }

    public getDocument() {
        return this.document;
    }
}