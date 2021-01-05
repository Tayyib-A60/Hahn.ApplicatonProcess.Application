import { PLATFORM } from 'aurelia-pal';
import { RouterConfiguration, Router } from "aurelia-router";

export class App {
  configureRouter(config: RouterConfiguration, router: Router) {
    config.options.pushState = true;

    const handleUnknownRoutes = (instruction) => {
      return { route: 'not-found', name: 'not-found', title: 'not-found', moduleId: PLATFORM.moduleName('pages/not-found'), nav: true };
  }

  config.mapUnknownRoutes(handleUnknownRoutes);

    config.map([
      { route: ['', 'apply'], name: 'apply', title: 'apply', moduleId: PLATFORM.moduleName('pages/apply'), nav: true },
      { route: 'success', name: 'success', title: 'success', moduleId: PLATFORM.moduleName('pages/success-page'), nav: true },
    ]);  
  }
}
