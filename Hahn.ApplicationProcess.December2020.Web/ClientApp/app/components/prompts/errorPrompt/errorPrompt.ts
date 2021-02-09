import { DialogController } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';

@inject(DialogController)
export class ErrorPrompt {
    private messages: string[] = new Array<string>();
    private hasErrors: boolean = false;
    constructor(public controller: DialogController) { }

    activate(response: any) {
        if (response.errors != null) {
            if (response.errors.hasOwnProperty("Name")) {
                this.messages.push(...response.errors.Name);
                this.hasErrors = true;
            }
            if (response.errors.hasOwnProperty("FamilyName")) {
                this.messages.push(...response.errors.FamilyName);
                this.hasErrors = true;
            }
            if (response.errors.hasOwnProperty("Address")) {
                this.messages.push(...response.errors.Address);
                this.hasErrors = true;
            }
            if (response.errors.hasOwnProperty("EmailAddress")) {
                this.messages.push(...response.errors.EmailAddress);
                this.hasErrors = true;
            }
            if (response.errors.hasOwnProperty("Age")) {
                this.messages.push(...response.errors.Age);
                this.hasErrors = true;
            }
            if (response.errors.hasOwnProperty("CountryOfOrigin")) {
                this.messages.push(...response.errors.CountryOfOrigin);
                this.hasErrors = true;
            }
        }
    }
}