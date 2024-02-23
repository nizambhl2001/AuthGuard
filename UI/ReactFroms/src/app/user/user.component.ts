import { Component, OnInit } from '@angular/core';
import { UserService } from './user.service';
import { UserStoreService } from '../service/user-store.service';
import { AuthService } from '../service/auth.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {

  Users:any[];
  public fullName:string = "";
  constructor(private UserService:UserService,private userStore:UserStoreService,private auth:AuthService) { }

  ngOnInit() {
   this.GetUser();

   this.userStore.getFullNameFromStore().subscribe(val=>{
    let fullNameToken = this.auth.getfullNameFromToken();
    this.fullName = val || fullNameToken
   })
  }
  GetUser(){
    this.UserService.getUser().subscribe((res)=>{
      this.Users = res
    });
  }
  
}
