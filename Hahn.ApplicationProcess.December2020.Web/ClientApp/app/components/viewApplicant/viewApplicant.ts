import { HttpClient } from 'aurelia-fetch-client';
import { inject } from 'aurelia-framework';
import { Router } from 'aurelia-router';
import { Applicant } from '../../applicant';
import { ApplicantDataService } from '../../applicantDataService';

@inject(ApplicantDataService, Router)
export class ViewApplicant {
    private applicant!: Applicant;

    constructor(private applicantDataService: ApplicantDataService) {
          
    }

    activate(params: any) {
        this.get(params.id);
    }

    get(id: number): void {
        if (isNaN(id)) {
            this.applicant = new Applicant();
            return;
        }
        this.applicantDataService.getById(id)
            .then(a => this.applicant = a);
    }
}