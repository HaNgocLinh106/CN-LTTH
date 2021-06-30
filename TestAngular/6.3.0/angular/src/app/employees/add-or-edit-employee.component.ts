import { AppComponentBase } from '@shared/app-component-base';
import { Component, EventEmitter, Injector, OnInit } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewChild } from '@angular/core';
import { Output } from '@angular/core';
import { Input } from '@angular/core';
import { CreateEmployeeInput, EmployeeServiceProxy, UpdateEmployeeInput } from '@shared/service-proxies/service-proxies';
import { finalize } from 'rxjs/operators';

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
  constructor(injector:Injector, private employeeService: EmployeeServiceProxy) { 
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
      .pipe(
        finalize(()=>{
        this.saving=false;
      }))
      .subscribe(result =>{
        this.modalSave.emit(result);
        this.close();
      });
    }
    else{
      console.log("birth = ", this.employeeEdit.birthDate);
      this.employeeService.updateEmployee(this.employeeEdit)
      .pipe(
        finalize(()=>{
        this.saving=false;
      }))
      .subscribe(result =>{
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
