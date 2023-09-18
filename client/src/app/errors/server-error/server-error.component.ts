import { Component, OnInit, numberAttribute } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.css']
})
export class ServerErrorComponent implements OnInit {

  error: any;

  constructor(private router: Router) {

    const navigation = this.router.getCurrentNavigation();
    this.error = navigation?.extras?.state?.['error'];  //'error' == error.interceptor.ts  inside of the case 500 name of the property


  }

  ngOnInit(): void {
  }

}
