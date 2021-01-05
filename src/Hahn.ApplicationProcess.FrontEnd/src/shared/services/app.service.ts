import { Options } from 'aurelia-loader-nodejs';
import { inject } from 'aurelia-dependency-injection';
import { HttpClient } from 'aurelia-http-client';
// import {HttpClient, json} from 'aurelia-fetch-client';
import { Applicant } from 'shared/models/applicant.model';
import * as environment from '../../../config/environment.json';

@inject(HttpClient)
export class AppService{

  httpClient = new HttpClient();

  constructor() {
    this.httpClient.configure(x => {
      x.withBaseUrl(environment.baseUrl);
      x.withHeader('Content-Type', 'application/json')
      x.withHeader('Accept', 'application/json')
    });
    
  }
  createApplicant(applicant: Applicant) {
    return this.httpClient.post('applicant', applicant).then(res => res.response);
  }

  get(){
    return this.httpClient.get('applicant');
  }
}
