import { Component, Input, OnInit } from '@angular/core';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  @Input() fullName: string; // Define input property
  @Input() role: string; 
  constructor(private auth:AuthService) { }

  ngOnInit() {
    
  }
  LogOut(){
    this.auth.logOut();
  }
}
