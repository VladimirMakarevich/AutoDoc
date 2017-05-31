(function (global) {
    System.config({
        paths: {

            'npm:': 'node_modules/'
        },

        map: {

            app: 'app',

            '@angular/core': 'npm:@angular/core/bundles/core.umd.js',
            '@angular/common': 'npm:@angular/common/bundles/common.umd.js',
            '@angular/compiler': 'npm:@angular/compiler/bundles/compiler.umd.js',
            '@angular/platform-browser': 'npm:@angular/platform-browser/bundles/platform-browser.umd.js',
            '@angular/platform-browser-dynamic': 'npm:@angular/platform-browser-dynamic/bundles/platform-browser-dynamic.umd.js',
            '@angular/http': 'npm:@angular/http/bundles/http.umd.js',
            '@angular/router': 'npm:@angular/router/bundles/router.umd.js',
            '@angular/forms': 'npm:@angular/forms/bundles/forms.umd.js',

            'rxjs': 'npm:rxjs',
            'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
            
            'ng-editable-table': 'npm:ng-editable-table',

            'ng2-smart-table': 'npm:ng2-smart-table',
            'ng2-smart-table/lib': 'npm:ng2-smart-table/build/src/ng2-smart-table/lib',
            'ng2-completer': 'npm:ng2-completer',
            'traceur': 'npm:traceur/bin/traceur.js',
            'lodash': 'npm:lodash',

            'ngx-bootstrap': 'npm:ngx-bootstrap',
            'ngx-bootstrap/buttons': 'npm:ngx-bootstrap/buttons',

            'ng2-file-upload': 'npm:ng2-file-upload',
            'ng2-file-uploader': 'npm:ng2-file-uploader',
            'ng2-uploader': 'npm:ng2-uploader',
            'ng2-uploader/src/classes': 'npm:ng2-uploader/src/classes',
        },

        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                main: 'bundles/Rx.js',
                defaultExtension: 'js'
            },
            /*'ng2-smart-table': {
                main: 'build/ng2-smart-table.js',
                meta: {
                    '*.html': {},
                    '*.scss': {}
                }
            },
            'ng2-smart-table/lib': {
                main: 'index.js'
            },
            'angular2-in-memory-web-api': {
                main: './index.js',
                defaultExtension: 'js'
            }*/
            /*'ng2-smart-table': {
                main: './index.js',
                defaultExtension: 'js'
            }*/
            'ng-editable-table': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            lodash: {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ng2-completer': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ng2-smart-table': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ngx-bootstrap': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ngx-bootstrap/buttons': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ng2-file-upload': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ng2-file-uploader': {
                format: 'register',
                defaultExtension: 'js'
            },
            'ng2-uploader': {
                main: 'index.js',
                defaultExtension: 'js'
            },
            'ng2-uploader/src/classes': {
                main: 'index.js',
                defaultExtension: 'js'
            }
        }
    });
})(this);