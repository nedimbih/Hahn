import { DialogService } from 'aurelia-dialog';
import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import {  ValidationRules, ValidationController, ValidationControllerFactory, validateTrigger } from 'aurelia-validation';
import { BindingSignaler } from 'aurelia-templating-resources';
import { ApplicantDataService } from '../../applicantDataService';
import { BootstrapFormRenderer } from '../../aureliaBootstrapFormRenderer';
import { ResetPrompt } from '../prompts/resetPrompt/resetPrompt';
import { RedirectPrompt } from '../prompts/redirectPrompt/redirectPrompt';
import { ErrorPrompt } from '../prompts/errorPrompt/errorPrompt';
import { Applicant } from '../../applicant';

@inject(ApplicantDataService, Router, BindingSignaler, DialogService, ValidationControllerFactory)
export class CreateApplicant {
    private applicant!: Applicant;
    private freshLoadFlagForResetButton: boolean;
    private freshLoadFlagForSendButton: boolean;
    private validationController: ValidationController;
    

    constructor(private applicantDataService: ApplicantDataService, private router: Router,
        private signaler: BindingSignaler, private dialog: DialogService,
        private controllerFactory: ValidationControllerFactory) {
        this.freshLoadFlagForResetButton = true;
        this.freshLoadFlagForSendButton = true;

        this.applicant = new Applicant();

        this.validationController = controllerFactory.createForCurrentScope();
        this.validationController.validateTrigger = validateTrigger.change;
        this.validationController.addRenderer(new BootstrapFormRenderer());
        
        ValidationRules.ensure((a: Applicant) => a.name).maxLength(100).minLength(5).required()
            .ensure((a: Applicant) => a.familyName).maxLength(100).minLength(5).required()
            .ensure((a: Applicant) => a.address).maxLength(200).minLength(10).required()
            .ensure((a: Applicant) => a.countryOfOrigin).maxLength(100).minLength(2).required()
            .ensure((a: Applicant) => a.emailAddress).email().maxLength(320).minLength(5).required()
            .ensure((a: Applicant) => a.age).range(20, 60).required()
            .on(this.applicant);
    }


    save(): void {
        this.applicantDataService.save(this.applicant)
            .then(rsp => this.handleResponse(rsp)) 
            .catch(error => {
                console.error("An error occurred when saving data!")
            });
    }

    private handleResponse(response: any): void {
        if (isNaN(response)) {
            this.dialog.open({ viewModel: ErrorPrompt, model: response, lock: false })
                .whenClosed(response => {
                    if (!response.wasCancelled) {
                        this.ResetAndRefresh();
                    }
                });
        }
        else {
            this.dialog.open({ viewModel: RedirectPrompt, model: response, lock: false })
                .whenClosed(resp => {
                    if (resp.wasCancelled) {
                        this.ResetAndRefresh();
                    }
                    else {
                        this.navigateToSavedApplicant(response);
                    }
                });
        }
    }

    private ResetAndRefresh(): void {
        document.getElementById("applicant-form").reset();
        this.SignalRefreshButtonStates();
    }

    navigateToSavedApplicant(id: string): void {
        let url = this.router.baseUrl + "#/viewApplicant/" + id;
        this.router.navigate(url);
    } 

    reset(): void {
        this.dialog.open({ viewModel: ResetPrompt, model: "??", lock: false })
            .whenClosed(response => {
                if (!response.wasCancelled) {
                    this.ResetAndRefresh();
                 }
            });
    }

    SignalRefreshButtonStates() {
        this.signaler.signal("refresh-send-button");
        this.signaler.signal("refresh-reset-button");
    }

     refreshSendButton(): boolean {
         let state: boolean = this.refreshButton(() =>
             (this.validationController.errors && this.validationController.errors.length > 0) ||
             document.getElementById("applicant-name").value == "" ||
             document.getElementById("applicant-familyname").value == "" ||
             document.getElementById("applicant-address").value == "" ||
             document.getElementById("applicant-country").value == "" ||
             document.getElementById("applicant-email").value == "" ||
             document.getElementById("applicant-age").value == ""
             );
         return state;
     }

     refreshResetButton(): boolean {
       
        return this.refreshButton(() => !(document.getElementById("applicant-name").value != "" ||
            document.getElementById("applicant-familyname").value != "" ||
            document.getElementById("applicant-address").value != "" ||
            document.getElementById("applicant-country").value != "" ||
            document.getElementById("applicant-email").value != "" ||
            document.getElementById("applicant-age").value != "" ||
            document.getElementById("applicant-hired").checked));
    }

    private refreshButton(Predicate: Function): boolean {
        if (this.freshLoadFlagForSendButton) {
            this.freshLoadFlagForSendButton = false;
            return true;
        }

        if (this.freshLoadFlagForResetButton) {
            this.freshLoadFlagForResetButton = false;
            return true;
        }

         return Predicate();
    }
}