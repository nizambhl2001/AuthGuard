import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-singup',
  templateUrl: './singup.component.html',
  styleUrls: ['./singup.component.css']
})
export class SingupComponent implements OnInit {
  type:string = "password";
  eyeIcon:string = "fa-eye-slash"
  isText:boolean;
  fromSingUp! :FormGroup;
  constructor(
    private fb:FormBuilder,
    private auth:AuthService,
    private router:Router, 
    ) { }

  ngOnInit() {
    this.createFroms();
  }
 

  createFroms(){
    this.fromSingUp = this.fb.group({
      firstname:['',[Validators.required]],
       lastname:['',[Validators.required]],
       username:['',[Validators.required]],
       email:['',[Validators.required,Validators.email]],
       Password:['',[Validators.required]]
    })
  }
  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = 'fa-eye' : this.eyeIcon = 'fa-eye-slash';
    this.isText ? this.type = 'text' : this.type = 'password'
  }

  get f(){
    return this.fromSingUp.controls
  }
  onSignUp(){
    if(this.fromSingUp.valid){
      this.auth.register(this.fromSingUp.value).subscribe(
        {
          next:(res=>{
            alert(res.message);
            this.fromSingUp.reset();
            this.router.navigate(['login']);
          }),
          error:(err=>{
            alert(err.error.message)
          })
        }
        
      )
      console.log(this.fromSingUp.value)
    }
    else{
      this.validateAllFromFields(this.fromSingUp);
    }
  }

  private validateAllFromFields(formGroup:FormGroup){
    Object.keys(formGroup.controls).forEach(field=>{
      const control = formGroup.get(field);
      if(control instanceof FormControl){
        control.markAsDirty({onlySelf:true});
      }
      else if(control instanceof FormGroup){
        this.validateAllFromFields(control);
      }
    })
  }
}
