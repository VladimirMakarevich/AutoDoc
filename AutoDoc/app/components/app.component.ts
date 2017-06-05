import { Component } from '@angular/core';

@Component({
    selector: 'autodoc-app',
    template: `<div class="container">
                    <div class="row">
                        <div class="col-md-12">
                                <router-outlet></router-outlet>
                        </div>
                    </div>                
                </div>`
})

export class AppComponent {
}