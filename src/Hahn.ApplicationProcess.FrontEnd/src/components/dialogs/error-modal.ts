import { inject } from 'aurelia-dependency-injection';
import {DialogController} from 'aurelia-dialog';

@inject(DialogController)
export class ErrorModal {
  errorMessage: string;
  controller = null;

  constructor(controller){
    this.controller = controller;
  }

  activate(errorMessage){
    this.errorMessage = errorMessage;
  }
}
