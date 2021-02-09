import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class ResetPrompt {

    constructor(public controller: DialogController) { }
}