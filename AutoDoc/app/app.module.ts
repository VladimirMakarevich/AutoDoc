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

const routes: Routes = [
    { path: 'bookmark/:id', component: BookmarkComponent },
    { path: 'download/:id', component: DownloadComponent },
    { path: 'upload/', component: UploadComponent }
];

const appRoutingProviders: any[] = [];

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        HttpModule,
        RouterModule.forRoot(routes)],
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