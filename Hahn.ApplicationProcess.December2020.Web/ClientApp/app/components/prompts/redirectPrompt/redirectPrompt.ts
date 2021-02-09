import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class RedirectPrompt {

    constructor(public controller: DialogController) {    }
}