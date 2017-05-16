import { Component, OnInit} from '@angular/core';

@Component({
    selector: 'autodoc-app',
    template: `<div>
                    <h1>AutDoc App</h1>
                    <router-outlet></router-outlet>
                </div>`,
})

export class AppComponent {
}