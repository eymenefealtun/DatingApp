import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  model: any = {}

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
  }


  login() {

    this.accountService.login(this.model).subscribe({

      next: () => this.router.navigateByUrl('/members'),

      // error: error => console.log(error)
      // error: error => this.toastr.error(error.error),
    });
  }

  logout() {
    this.accountService.logout(); //that is going to remove the item from local storage 
    this.router.navigateByUrl('/'); //navigates to the home page
  }

}
