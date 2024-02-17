import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  Users:any[];
  constructor(private UserService:UserService) { }

  ngOnInit() {
   this.GetUser();
  }
  GetUser(){
    this.UserService.getUser().subscribe((res)=>{
      this.Users = res
    });
  }
}
