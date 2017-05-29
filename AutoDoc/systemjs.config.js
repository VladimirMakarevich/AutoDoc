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

            'ng2-smart-table': 'npm:ng2-smart-table',
            'ng2-smart-table/lib': 'npm:ng2-smart-table/build/src/ng2-smart-table/lib',

            'rxjs': 'npm:rxjs',
            'angular-in-memory-web-api': 'npm:angular-in-memory-web-api/bundles/in-memory-web-api.umd.js',
            'rxjs': 'node_modules/rxjs',
            'traceur': 'npm:traceur/bin/traceur.js',
            '@angular': 'node_modules/@angular',
            'lodash': 'npm:lodash',
        },

        packages: {
            app: {
                main: './main.js',
                defaultExtension: 'js'
            },
            rxjs: {
                defaultExtension: 'js'
            },
            'angular2-in-memory-web-api': {
                main: './index.js',
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
            }
        }
    });
})(this);