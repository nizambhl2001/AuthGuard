import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/service/auth.service';
import { UserStoreService } from 'src/app/service/user-store.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  type:string = "password";
  eyeIcon:string = "fa-eye-slash"
  isText:boolean;
  formLogin!:FormGroup;
  constructor(
    private fb:FormBuilder,
    private auth:AuthService,
    private  router:Router,
    private userStore:UserStoreService,
    ) { }

  ngOnInit() {
   this.createFroms();
  }
  hideShowPass(){
    this.isText = !this.isText;
    this.isText ? this.eyeIcon = 'fa-eye' : this.eyeIcon = 'fa-eye-slash';
    this.isText ? this.type = 'text' : this.type = 'password'
  }
  createFroms(){
    this.formLogin =this.fb.group({
      Username:['',Validators.required],
      Password:['',Validators.required]
    })
  }
  onLogin(){
    if(this.formLogin.valid){
      console.log(this.formLogin.value)
      this.auth.login(this.formLogin.value).subscribe(
        {
          next:(res)=>{
           alert(res.message);
           this.formLogin.reset();
           this.auth.storeToken(res.token);
           const tokenPayload = this.auth.decodedToken();
           this.userStore.setFullNameFromStore(tokenPayload.name);
           this.userStore.setRoleFromStore(tokenPayload.role);
           this.router.navigate(['dashboard']);
          },
          error:(err=>{
            alert(err.message);
            console.log();
          })
        }
       )
      
    }
    else{
      this.validateAllFromFields(this.formLogin);
      alert('Form not valid');
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
  get f(){
    return this.formLogin.controls;
  }

}
