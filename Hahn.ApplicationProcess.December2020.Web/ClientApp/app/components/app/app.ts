import { Aurelia, PLATFORM } from 'aurelia-framework';
import { Router, RouterConfiguration } from 'aurelia-router';

export class App {
    router!: Router;

    configureRouter(config: RouterConfiguration, router: Router) {
        config.title = 'Aurelia';
        config.map([{
                route: ["", 'createApplicant'],
                name: 'createApplicant',
                settings: { icon: 'plus' },
                moduleId: PLATFORM.moduleName('../createApplicant/createApplicant'),
                nav: true,
                title: 'Create applicant'
        }, {
                route: "viewApplicant/:id",
                name: 'viewApplicant',
                href: "#viewApplicant",
                settings: { icon: 'minus' },
                moduleId: PLATFORM.moduleName('../viewApplicant/viewApplicant'),
                nav: false,
                title: 'View applicant'
            }]);

        this.router = router;
    }
}
