import {Aurelia} from 'aurelia-framework';
import * as environment from '../config/environment.json';
import {PLATFORM} from 'aurelia-pal';
import { TCustomAttribute } from 'aurelia-i18n';
import Backend from 'i18next-xhr-backend';

export function configure(aurelia: Aurelia) {
  aurelia.use
    .standardConfiguration()
    .feature(PLATFORM.moduleName('resources/index'))
    .plugin(PLATFORM.moduleName('aurelia-validation'))
    .plugin(PLATFORM.moduleName('aurelia-dialog'))
    .plugin(PLATFORM.moduleName('aurelia-i18n'), (instance) => {
      let aliases = ['t', 'i18n'];
      // add aliases for 't' attribute
      TCustomAttribute.configureAliases(aliases);

      // register backend plugin
      instance.i18next.use(Backend);


      // adapt options to your needs (see http://i18next.com/docs/options/)
      // make sure to return the promise of the setup method, in order to guarantee proper loading
      return instance.setup({
        backend: {
          loadPath: './locales/{{lng}}/{{ns}}.json',
        },
        attributes: aliases,
        lng : 'de',
        fallbackLng : 'en',
        ns: ['nav', 'translation'],
        defaultNs: 'translation',
        debug : false
      });
    });

  aurelia.use.developmentLogging(environment.debug ? 'debug' : 'warn');

  if (environment.testing) {
    aurelia.use.plugin(PLATFORM.moduleName('aurelia-testing'));
  }

  aurelia.start().then(() => aurelia.setRoot(PLATFORM.moduleName('app')));
}
