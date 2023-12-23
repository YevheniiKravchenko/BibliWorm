import { Component } from '@angular/core';
import { StorageService } from '../../../core/services/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
    
    constructor(
        private storageService: StorageService,
        private router: Router
    ) { }

    onButtonClick(event: Event): void {
        var currentButton = <HTMLElement>event.currentTarget;
        if (currentButton.classList.contains('active')) {
            return;
        }

        this.disativateButtons();
        
        currentButton.classList.add('active');
    }

    disativateButtons(): void {
        var buttons = document.querySelectorAll('header ul button');

        for (let i = 0; i < buttons.length; i++) {
            buttons[i].classList.remove('active');
        }
    }

    isAdminOrLibrarian(): boolean {
        return this.storageService.isLibrarianOrAdmin();
    }

    isAdmin(): boolean {
        return this.storageService.isAdmin();
    }

    isLoggedIn(): boolean {
        return this.storageService.isLoggedIn();
    }

    logout(): void {
        this.storageService.clear();
        this.router.navigate(['login']);
    }
}
