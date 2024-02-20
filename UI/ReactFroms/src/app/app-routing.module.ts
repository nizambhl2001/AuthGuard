import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './component/login/login.component';
import { SingupComponent } from './component/singup/singup.component';
import { EmployeeComponent } from './employee/employee.component';
import { AuthGuard } from './guard/auth.guard';
import { UserComponent } from './user/user.component';


const routes: Routes = [
  { path:'', component:LoginComponent },
  { path:'login', component:LoginComponent },
  { path:'signup', component:SingupComponent},
  { path:'dashboard' ,component:EmployeeComponent,canActivate:[AuthGuard]},
  { path:'user' ,component:UserComponent,canActivate:[AuthGuard]}
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
