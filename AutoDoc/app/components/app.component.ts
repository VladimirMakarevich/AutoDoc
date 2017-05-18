import { Component, OnInit } from '@angular/core';

@Component({
    selector: 'autodoc-app',
    template: `<div>
                    <div>
                        <h2>AutDoc App</h2>
                    </div>
                    <router-outlet></router-outlet>
                </div>`,
})

export class AppComponent {
}