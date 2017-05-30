import { Component, Input, OnInit } from '@angular/core';
import { ViewCell } from 'ng2-smart-table';


@Component({
  template: `
    <button (click)="example()">Click me</button>
  `,
})
export class ButtonRenderComponent implements OnInit {

  public renderValue: any;

  @Input() value: any;

  constructor() {  }

  ngOnInit() {
    this.renderValue = this.value;
  }

  example() {
    alert(this.renderValue);
  }
}