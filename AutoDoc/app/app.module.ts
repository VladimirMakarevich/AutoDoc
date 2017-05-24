import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { APP_BASE_HREF } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { AppComponent }   from './components/app.component';
import { UploadComponent } from "./components/document/upload.component";
import { BookmarkComponent } from "./components/document/bookmark.component";
import { DownloadComponent } from "./components/document/download.component";
import { DocumentService} from "./services/document/document.service";
import { BookmarkService } from "./services/document/bookmark.service";
import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';

const appRoutes: Routes = [
    { path: '', redirectTo: '/upload', pathMatch: 'full' },
    { path: 'upload', component: UploadComponent, pathMatch: 'full' },
    { path: 'bookmark/:id', component: BookmarkComponent },
    { path: 'download/:id', component: DownloadComponent }
];

const appRoutingProviders: any[] = [];

@NgModule({
    imports: [
        Ng2SmartTableModule,
        LocalDataSource,
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot(appRoutes)],
    providers: [
        { provide: APP_BASE_HREF, useValue: '/' },
        DocumentService,
        BookmarkService
    ],
    declarations: [
        AppComponent,
        UploadComponent,
        DownloadComponent,
        BookmarkComponent
    ],
    exports: [
        AppComponent,
        RouterModule
    ],
    bootstrap: [
        AppComponent
    ],
})

export class AppModule { }