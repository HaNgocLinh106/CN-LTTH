import { TaskServiceProxy, EmployeeServiceProxy } from './../../shared/service-proxies/service-proxies';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { ModalModule, TabsModule, TooltipModule } from 'ngx-bootstrap';
import { AppCommonModule } from '@app/shared/common/app-common.module';
import { UtilsModule } from '@shared/utils/utils.module';
import { MainRoutingModule } from './main-routing.module';
import { CountoModule } from '@node_modules/angular2-counto';
import { EasyPieChartModule } from 'ng2modules-easypiechart';
import { TasksComponent } from './tasks/tasks.component';
import { EmployeesComponent } from './employees/employees.component';
// import { AddOrEditTaskModalComponent } from './tasks/add-or-edit-task-modal.component';



@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ModalModule,
        TabsModule,
        TooltipModule,
        AppCommonModule,
        UtilsModule,
        MainRoutingModule,
        CountoModule,
        EasyPieChartModule,

    ],
    declarations: [
        DashboardComponent,
        TasksComponent,
        EmployeesComponent,
        // AddOrEditTaskModalComponent,
    ],
    providers:[
        TaskServiceProxy,
        EmployeeServiceProxy
    ]
})
export class MainModule { }
