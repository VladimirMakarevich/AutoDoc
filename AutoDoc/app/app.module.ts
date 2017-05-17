import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule }   from '@angular/forms';
import { APP_BASE_HREF } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { HttpModule }   from '@angular/http';
import { AppComponent }   from './components/app.component';

const routes: Routes =[
];

const appRoutingProviders: any[] = [
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpModule, RouterModule.forRoot(routes)],
	providers: [{provide: APP_BASE_HREF, useValue : '/' }],
    declarations: [AppComponent],
    exports: [AppComponent, RouterModule ],
    bootstrap: [AppComponent],
})

export class AppModule { }