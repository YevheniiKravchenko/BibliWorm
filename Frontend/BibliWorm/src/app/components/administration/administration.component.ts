import { Component } from '@angular/core';
import { AdministrationService } from '../../core/services/administration.service';
import { ToastrService } from 'ngx-toastr';
import { setActiveButton } from '../../core/helpers/helpers';

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrl: './administration.component.scss'
})
export class AdministrationComponent {

    savePath: string = ''

    constructor(
        private administrationService: AdministrationService,
        private toastr: ToastrService
    ) { }

    ngAfterViewInit(): void {
        setActiveButton('#administrationBtn');
    }

    onBackupDatabaseButtonClick(): void {
        if (this.savePath === '') {
            return;
        }

        this.administrationService.backupDatabase(this.savePath).subscribe({
            next: () => {
                this.toastr.success('Success', 'Operation successful');
            },
            error: (error) => {
                this.toastr.error(error.error['message'], 'Operation error');  
            }
        });
    }

}
