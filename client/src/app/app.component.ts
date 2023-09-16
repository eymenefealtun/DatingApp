import { Component, OnInit } from '@angular/core';
import { AccountService } from './_services/account.service';
import { User } from './_models/user';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit { //we've added a lifecycle event by 'implements OnInit'. Impelmented this interface to our class
  title = 'Dating app';

  constructor(private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.setCurrentUser();
  }


  setCurrentUser() {
    // const user: User = JSON.parse(localStorage.getItem('user')!);
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user: User = JSON.parse(userString);

    this.accountService.setCurrentUser(user);
  }


}
