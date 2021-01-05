import { inject } from 'aurelia-dependency-injection';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class Reset {
  text: string;
  resetProps = { text: '' };
  controller = null;

  constructor(controller){
    this.controller = controller;
  }

  activate(resetProps){
    this.resetProps = resetProps;
  }
}
