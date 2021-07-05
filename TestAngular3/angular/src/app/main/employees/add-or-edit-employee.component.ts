
import { Component, EventEmitter, Injector, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewChild } from '@angular/core';
import { Output } from '@angular/core';
import { Input } from '@angular/core';
import { CreateEmployeeInput, EmployeeServiceProxy, TaskServiceProxy, UpdateEmployeeInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';

@Component({
  selector: 'addOrEditEmployee',
  templateUrl: './add-or-edit-employee.component.html',
  styleUrls: ['./add-or-edit-employee.component.css']
})
export class AddOrEditEmployeeComponent extends AppComponentBase implements OnInit {

@ViewChild('createOrEditEmployeeModal') modal:ModalDirective;
@Input() employeeEdit:UpdateEmployeeInput;

@Output() modalSave: EventEmitter<any> = new EventEmitter<any>();
active= false;
saving= false;
employee: CreateEmployeeInput= new CreateEmployeeInput();
  constructor(injector:Injector, private employeeService: EmployeeServiceProxy, private taskService: TaskServiceProxy) { 
    super(injector);
      this.employeeEdit=null;
  }

  Show():void{
    this.active= true;
    this.modal.show();
  }
  save():void{
    this.saving=true;
    if(this.employeeEdit==null){
      this.employeeService.createEmployee(this.employee)
      // .pipe(
      //   finalize(()=>{
       
      // }))
      .subscribe(result =>{
        this.saving=false;
        this.modalSave.emit(result);
        this.close();
         this.employee.name="";
      });
    }
    else{

      this.employeeService.updateEmployee(this.employeeEdit)
      // .pipe(
      //   finalize(()=>{
       
      // }))
      .subscribe(result =>{
        this.saving=false;
        this.modalSave.emit(result);
        this.close();
      });
    }
   
  }
  close(){
    this.active=false;
    this.modal.hide();
    this.employeeEdit=null;
  }
  ngOnInit(): void {
  }


}
