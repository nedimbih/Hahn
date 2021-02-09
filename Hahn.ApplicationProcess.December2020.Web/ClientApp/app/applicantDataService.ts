import { inject } from "aurelia-framework";
import { HttpClient, json } from "aurelia-fetch-client";
import { Applicant } from "./applicant";

@inject(HttpClient, json)
export class ApplicantDataService {

    constructor(private http: HttpClient) {
        
    }

    getById(id: number): Promise<Applicant>{
         return this.http.fetch(`api/Applicant/${id}`)
             .then(response => response.json());
    }

    save(applicant: Applicant): Promise<string> {
        return this.http.fetch("api/Applicant", {
            method: 'post',
            body: json(applicant)
        })
            .then(result => result.json());
    }
}