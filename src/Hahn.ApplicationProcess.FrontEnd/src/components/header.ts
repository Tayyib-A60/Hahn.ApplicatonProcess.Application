import { autoinject, inject, signalBindings } from 'aurelia-framework';
import {I18N} from 'aurelia-i18n';
import {BindingSignaler} from 'aurelia-templating-resources';

@autoinject
export class Header {
  appTitle = "Hanh";
  i18N = null;
  signaler: BindingSignaler;

  constructor(i18N: I18N, signaler: BindingSignaler) {
    this.i18N = i18N;
    this.signaler = signaler;
  }

  setLocale(locale) {
    return new Promise( resolve => {
      let oldLocale = this.i18N.getLocale();
      console.log(oldLocale);
      this.i18N.setLocale(locale, tr => {
        this.signaler.signal('aurelia-translation-signal');
        resolve(tr);
      });
      signalBindings('locale-changed');
    });
  }
}
