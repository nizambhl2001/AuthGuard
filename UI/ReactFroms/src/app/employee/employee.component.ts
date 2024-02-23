import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { EmpModelobj } from './Emp.Models';
import { EmpService } from '../emp.service';
import { AuthService } from '../service/auth.service';
import { UserStoreService } from '../service/user-store.service';


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css']
})

export class EmployeeComponent implements OnInit {
  @Input() fullName: string; // Define input property 
  @Input() role: string; 
  addItemForm!:FormGroup;
  addEmployeeModelObj :EmpModelobj = new EmpModelobj();
  getdata:any[] = [];
  districts:any[];
  genders:string[]= ["Male","Female","Other"];
  religions:any[];
  ShowAdd!:boolean;
  ShowUpdate!:boolean;
  formSubmitted = false;
  searchText: string = '';
  currentPage: number = 1;
  itemsPerPage: number = 3;
  

  constructor(
    private httpService:EmpService,
    private auth:AuthService,
    private fb: FormBuilder,
    private userStore:UserStoreService,
   
    ){}

   ngOnInit(){
   this.GetEmpData();
   this. getReligions();
   this.getDistricts();
   this.CreateFroms();
   this.userStore.getFullNameFromStore().subscribe(val=>{
    let fullNameToken = this.auth.getfullNameFromToken();
    this.fullName = val || fullNameToken
   })

   this.userStore.getRoleFromStore().subscribe(val=>{
    let RoleToken = this.auth.getroleFromToken();
    this.role = val || RoleToken
   })

  }

 
  //reactive froms
  CreateFroms():void{
    this.addItemForm = this.fb.group({
      fname: ['',[Validators.required]],
      lname: [''],
      email: [''],
      mobile: ['' ],
      salary: [''],
      district:[''],
      gender:[''],
      religion:['']
   
    });
   }
   // get religion arry
  getReligions(){
    this.httpService.getReligions().subscribe((reli)=>{this.religions = reli})
  }

  // get religion distric
  getDistricts(){
    this.httpService.getDistricts().subscribe(data => {
      this.districts = data;
    });
  }

  ClickAddEmployee(){
    this.addItemForm.reset();
    this.ShowAdd = true;
    this.ShowUpdate = false;
  }

 //Employee data
  GetEmpData(){
    this.httpService.getdata()
    .subscribe((data)=>{this.getdata = data})
  }
  

  //employee save method
  onSubmit() {
    this.formSubmitted = true
    if (this.addItemForm.valid) {
     
        this.addEmployeeModelObj.fname = this.addItemForm.value.fname,
        this.addEmployeeModelObj.lname = this.addItemForm.value.lname,
        this.addEmployeeModelObj.district = this.addItemForm.value.district,
        this.addEmployeeModelObj.gender = this.addItemForm.value.gender,
        this.addEmployeeModelObj.religion = this.addItemForm.value.religion,
        this.addEmployeeModelObj.email = this.addItemForm.value.email,
        this.addEmployeeModelObj.mobile= this.addItemForm.value.mobile,
        this.addEmployeeModelObj.salary = this.addItemForm.value.salary

      this.httpService.addItem(this.addEmployeeModelObj).subscribe(() => {
       alert('Employee added successfully');
        document.getElementById('cancel').click();
        this.GetEmpData();
       // this.addItemForm.reset();
      });
    
    }
    else{
      alert('something worg');
    }
   
  }
//set employee update value
  onEdit(item:any){
    this.ShowAdd = false;
    this.ShowUpdate = true;
    this.addEmployeeModelObj.id = item.id
    this.addItemForm.controls['fname'].setValue(item.fname);
    this.addItemForm.controls['lname'].setValue(item.lname);
    this.addItemForm.controls['district'].setValue(item.district);
    this.addItemForm.controls['gender'].setValue(item.gender);
    this.addItemForm.controls['religion'].setValue(item.religion);
    this.addItemForm.controls['email'].setValue(item.email);
    this.addItemForm.controls['mobile'].setValue(item.mobile);
    this.addItemForm.controls['salary'].setValue(item.salary);

  }

//employee update method
  UpdateEmployee() {
    if (this.addItemForm.valid) {
      
        this.addEmployeeModelObj.fname = this.addItemForm.value.fname,
        this.addEmployeeModelObj.lname = this.addItemForm.value.lname,
        this.addEmployeeModelObj.district = this.addItemForm.value.district,
        this.addEmployeeModelObj.gender = this.addItemForm.value.gender,
        this.addEmployeeModelObj.religion = this.addItemForm.value.religion,
        this.addEmployeeModelObj.email = this.addItemForm.value.email,
        this.addEmployeeModelObj.mobile= this.addItemForm.value.mobile,
        this.addEmployeeModelObj.salary = this.addItemForm.value.salary

      this.httpService.updateItem(this.addEmployeeModelObj,this.addEmployeeModelObj.id).subscribe(() => {
       alert('Update Employee successfully');
        document.getElementById('cancel').click();
        this.GetEmpData();
        this.addItemForm.reset();
      });
    }
    
   
  }
//delete update method
  DeleteItem(item:any){
    this.httpService.DeleteItem(item.id).subscribe((res)=>{
      alert('Are Your sure Employee Delete');
      this.GetEmpData();
    });
  }

  get f(){
    return this.addItemForm.controls;
  }

  //pagination seciton and search
  get paginatedData(): any[] {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    return this.getdata.slice(startIndex, startIndex + this.itemsPerPage);
  }

  // Pagination methods
  nextPage() {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
    }
  }

  prevPage() {
    if (this.currentPage > 1) {
      this.currentPage--;
    }
  }

  get totalPages(): number {
    return Math.ceil(this.getdata.length / this.itemsPerPage);
  }

  // Method to reset search input
  resetSearchInput(){
    this.searchText = '';
  }
  }


