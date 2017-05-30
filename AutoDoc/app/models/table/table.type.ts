import { Ng2SmartTableModule, LocalDataSource } from 'ng2-smart-table';

export class Table {
    constructor() {
        this.settings = new Settings();
        this.data = new LocalDataSource();
    }

    settings: Settings;
    data: any;
}

export class Settings {
    constructor() {
        this.columns = new Object;
        this.mode = 'inline';
    }

    mode: string;
    columns: any;
}