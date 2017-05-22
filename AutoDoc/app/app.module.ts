import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { APP_BASE_HREF } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { AppComponent } from './components/app.component';
import { DocumentComponent } from './components/document/upload/document.component';
import { DownDocumentComponent } from './components/document/download/downdocument.component';
import { BookmarkComponent } from './components/bookmarks/bookmarks.component';
import { ErrorComponent } from './components/error/error.component';

const routes: Routes = [
    { path: '', component: AppComponent },
    { path: 'document', component: DocumentComponent },
    { path: 'bookmarks', component: BookmarkComponent },
    { path: '**', component: ErrorComponent }
];

const appRoutingProviders: any[] = [
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, RouterModule.forRoot(routes)],
	providers: [{provide: APP_BASE_HREF, useValue : '/' }],
    declarations: [AppComponent,
        DocumentComponent,
        ErrorComponent],
    exports: [AppComponent, RouterModule ],
    bootstrap: [AppComponent],
})

export class AppModule { }