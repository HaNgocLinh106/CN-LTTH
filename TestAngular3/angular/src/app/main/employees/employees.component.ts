
import { Component, Injector, Input, OnInit, ViewChild } from '@angular/core';
import { EmployeeListDetailDto, EmployeeListDto, EmployeeServiceProxy, TaskServiceProxy } from '@shared/service-proxies/service-proxies';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { AddOrEditEmployeeComponent } from './add-or-edit-employee.component';
import { Console } from 'console';
import { AppComponentBase } from '@shared/common/app-component-base';


@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent extends AppComponentBase implements OnInit {
  employees:EmployeeListDetailDto[]=[];
  employeeEdit: EmployeeListDto;
  employeeDelete:EmployeeListDetailDto;
  first = 0;
@Input() employeeCheck=false;
  rows = 10;
  selectedIDs:any[]=[];
  checkDelete:boolean;
  masterCheck:boolean;
  listEmployee:EmployeeListDetailDto[]=[];
  listEmployeeId:any[]=[];
  multiSortMeta:any[]=[];
     @ViewChild('addOrEditEmployeeModal') addOrEditEmployeeModal:AddOrEditEmployeeComponent;
    @ViewChild('deleteEmployeeModal') deleteEmployeeModal:ModalDirective;
  constructor(injector:Injector, private employeeService: EmployeeServiceProxy, private taskService: TaskServiceProxy) { 
    super(injector);
    this.employeeEdit=null;
    this.employeeCheck=false;
    this.masterCheck=false;
    this.checkDelete= true;
    this.getEmployees();
  }

  ngOnInit(): void {
   this.getEmployees();
   this.employeeEdit=null;
   this.masterCheck=false;
   this.multiSortMeta = [];
   this.multiSortMeta.push({field: 'id', order: 1});
  //  this.multiSortMeta.push({field: 'name', order: -1});
  }

  //paging

  next() {
    this.first = this.first + this.rows;
}

prev() {
    this.first = this.first - this.rows;
}

reset() {
    this.first = 0;
}

isLastPage(): boolean {
    return this.employees ? this.first === (this.employees.length - this.rows): true;
}

isFirstPage(): boolean {
    return this.employees ? this.first === 0 : true;
}
  getEmployees(){
    this.employeeService.getListEmployee().subscribe(result =>{
      this.employees= result;
     
    });
  }
  getEmployee(employee:EmployeeListDto){
      this.employeeService.getEmployee(employee.id)
      .subscribe(result =>{
       this.setEmployeeEdit(result);
       
      });
      this.showEmployeeModal();
     
  }
  // deleteEmployee(employee:EmployeeListDto){
  //   this.deleteEmployeeModal.show();
  //   this.employeeDelete= employee;
  // }
  deleteEmployee(){
  // this.checkDelete=false;//xoa duoc
  if(this.listEmployee.length>0)
  {
    this.checkDelete=true;//xoa duoc
  }
  else{
    this.checkDelete=false; // khong xoa duoc
  }


    this.deleteEmployeeModal.show();
    // this.employeeEdit=employee;
    // this.employeeDelete= employee;
  }
  
accept(){
 
  this.listEmployee.forEach(obj=>{
  
     this.listEmployeeId.push(obj.id);
  });
  console.log("list id=", this.listEmployeeId);
  this.employeeService.deleteEmployee(this.listEmployeeId)
    .subscribe(result =>{
      this.listEmployee=[];
      this.listEmployeeId=[];
      this.getEmployees();
    this.deleteEmployeeModal.hide();
    this.masterCheck=false;
    this.notify.success(this.l('DeletedSuccessully'));
   });
  }

  setEmployeeEdit(value: any) {
    this.employeeEdit = value;
     this.employeeEdit.birthDate=value["birthDate"]? value["birthDate"].format("YYYY-MM-DD"):<any>undefined;
}

selectID(employee: EmployeeListDetailDto, event:any){
  if(employee==null){
        this.masterCheck=!this.masterCheck;
        if(event.target.checked){
      this.employees.forEach(obj=>{
        this.listEmployee.push(obj);
      })
        }
        else{
          this.listEmployee=[];
        }
  }
  else{
        if(event.target.checked){
          this.listEmployee.push(employee);
        }
        else{
          // this.masterCheck=false;
          let index = this.listEmployee.indexOf(employee);
          this.listEmployee.splice(index,1);
          
        }
       }
 
}

  showEmployeeModal(): void{
    this.addOrEditEmployeeModal.show();
  }

 onEmployeeUpdated(input:any){
   if(this.employeeEdit==null){
    this.employees.push(input);
   
   }
  
   this.notify.success(this.l('SavedSuccessully'));
   this.getEmployees();
 }
 cancel(){
  this.deleteEmployeeModal.hide();
}

//sort
employeeSort(event: any) {
  event.data.sort((data1, data2) => {
      let value1 = data1[event.field];
      let value2 = data2[event.field];
      let result = null;

      if (value1 == null && value2 != null)
          result = -1;
      else if (value1 != null && value2 == null)
          result = 1;
      else if (value1 == null && value2 == null)
          result = 0;
      else if (typeof value1 === 'string' && typeof value2 === 'string')
          result = value1.localeCompare(value2);
      else
          result = (value1 < value2) ? -1 : (value1 > value2) ? 1 : 0;

      return (event.order * result);
  });
  
}
}

