import { ErrorModal } from './dialogs/error-modal';
import { AppService } from './../shared/services/app.service';
import { inject } from 'aurelia-dependency-injection';
import {Router} from 'aurelia-router';
import { ValidationControllerFactory, ValidationRules, Validator} from 'aurelia-validation';
import { BootstrapFormRenderer } from 'resources/renderer/bootstrap-form-renderer';
import { Applicant } from 'shared/models/applicant.model';
import { DialogService } from 'aurelia-dialog';
import { Reset } from './dialogs/reset';
import { I18N } from 'aurelia-i18n';
import { BindingSignaler } from 'aurelia-templating-resources';

@inject(ValidationControllerFactory, Validator, AppService, DialogService, I18N, BindingSignaler, Router)
export class CreateApplicant {
  applicant: Applicant;
  validator = null;
  controller = null;
  errorStyle: string;
  renderer = new BootstrapFormRenderer();
  appService = null;
  canReset = false;
  dialogService = null;
  i18N = null;
  signaler: BindingSignaler;
  router: Router;
  
  constructor(controllerFactory: ValidationControllerFactory, validator: Validator, appService: AppService, dialogService: DialogService, i18N: I18N, signaler: BindingSignaler, router: Router) {
    this.validator = validator;
    this.controller = controllerFactory.createForCurrentScope();
    this.appService = appService;
    this.dialogService = dialogService;
    this.i18N = i18N;
    this.signaler = signaler;
    this.router = router;
    this.applicant = <Applicant>{ name: '', address: '', emailAddress: '', age: null, countryOfOrigin: '', familyName: '', hired: false }

    this.controller.addRenderer(this.renderer);
    this.setValidation();
  }

  submit() {
    this.controller.validate()
      .then(validationResult => {
        if(validationResult.valid) {
          this.appService.createApplicant(this.applicant).then(data => {
            console.log(data);
            this.router.navigate(`success`);
          }).catch((err) => {
            const errObject = JSON.parse(err.response);
            let message = errObject?.title + '\n';

            Object.values(errObject.errors).map((val: Array<string>) => {
              val.forEach(msg => {
                message += msg + '\n';
              });
            });

            this.dialogService.open({ viewModel: ErrorModal, model: message, lock: true }).whenClosed(response => {
              if (!response.wasCancelled) {
                // Close modal
              }
            });
          });
        }
      });
  }

  setLocale(locale) {
    return new Promise( resolve => {
      this.i18N.setLocale(locale, tr => {
        this.signaler.signal('aurelia-translation-signal');
        resolve(tr);
      });
    });
  }

  reset() {
    this.dialogService.open({ viewModel: Reset, model: {text: 'Are you sure you want to reset the current data?'}, lock: true }).whenClosed(response => {
      if (!response.wasCancelled) {
        this.applicant = <Applicant>{ name: '', address: '', emailAddress: '', age: null, countryOfOrigin: '', familyName: '', hired: false };
        this.controller.validate();
      } else {
        // Do nothing
      }
    });
  }

  get disableReset() {
    const disable = !this.applicant.address && !this.applicant.name && !this.applicant.countryOfOrigin && !this.applicant.countryOfOrigin && !this.applicant.emailAddress && !this.applicant.familyName && !this.applicant.age && !this.applicant.hired;
    
    return disable;
  }

  private setValidation() {
    ValidationRules
      .ensure('emailAddress').required().email()
      .ensure('name').required().minLength(5)
      .ensure('familyName').required().minLength(5)
      .ensure('countryOfOrigin').required()
      .ensure('address').required().minLength(10)
      .ensure('age').required().min(20).withMessage('Minimum age required is 20 years')
      .max(60).withMessage('Maximum age required is 60 years')
      .on(this.applicant);

      this.controller.validate();
  }
}
