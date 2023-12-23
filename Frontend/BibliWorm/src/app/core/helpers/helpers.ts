import { HttpClient } from "@angular/common/http";
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function setActiveButton(buttonSelector: string) {
    setTimeout(() => {
        activateButton(buttonSelector);
    }, 50);
}

function activateButton(buttonSelector: string) {
    var button = document.querySelector(buttonSelector);

    if (button == null || button == undefined) {
        return;
    }

    if (button.classList.contains('active')) {
        return;
    }

    disativateButtons();
    button.classList.add('active');
}

function disativateButtons(): void {
    var buttons = document.querySelectorAll('header ul button');

    for (let i = 0; i < buttons.length; i++) {
        buttons[i].classList.remove('active');
    }
}

export function createTranslateLoader(http: HttpClient) {
    return new TranslateHttpLoader(http, './assets/i18n/', '.json');
}