import { Injectable } from '@angular/core';

export interface File {
    fileDownloadName: string;
    fileContents: Blob;
}