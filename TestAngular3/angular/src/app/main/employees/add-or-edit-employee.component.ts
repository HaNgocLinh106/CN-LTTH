
import { Component, EventEmitter, Injector, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewChild } from '@angular/core';
import { Output } from '@angular/core';
import { Input } from '@angular/core';
import { CreateEmployeeInput, EmployeeListDto, EmployeeServiceProxy, TaskServiceProxy, UpdateEmployeeInput } from '@shared/service-proxies/service-proxies';
import { AppComponentBase } from '@shared/common/app-component-base';
import * as moment from 'moment';

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
employee: EmployeeListDto= new EmployeeListDto();
  constructor(injector:Injector, private employeeService: EmployeeServiceProxy, private taskService: TaskServiceProxy) { 
    super(injector);
      this.employeeEdit=null;
  }

  show(employeeId?:number):void{
    if (employeeId) {
      //Edit
      this.employeeService.getEmployee(employeeId).subscribe((result) => {
        this.employee =  result;
        this.employee.birthDate = this.employee.birthDate  ?this.employee.birthDate.format('YYYY-MM-DD') : <any>undefined;
      });

    } else {
      //Create
      this.employee = new EmployeeListDto();
    }

    this.active= true;
    this.modal.show();
    console.log("test=",this.employee);
  }
  save():void{ 
    this.employee.birthDate = this.employee.birthDate  ? moment(this.employee.birthDate.toString()) : <any>undefined;
    this.saving=true;
    if(this.employee.id==null){
      this.employeeService.createEmployee(<any>this.employee)
      .subscribe(result =>{
        this.saving=false;
        this.modalSave.emit(result);
        this.close();
        this.employee.name="";
      });
    }
    else{
      this.employeeService.updateEmployee(this.employee)
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
