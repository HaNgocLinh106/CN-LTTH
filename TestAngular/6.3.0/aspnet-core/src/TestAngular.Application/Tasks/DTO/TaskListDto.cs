﻿
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
using Acme.SimpleTaskApp.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using TestAngular.Tasks.Configuration;

namespace TestAngular.Tasks.DTO
{
    [AutoMapFrom(typeof(Task))]
   public class TaskListDto:EntityDto, IHasCreationTime
    {
        public TaskListDto()
        {

        }
        public int TenantId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public TaskState State { get; set; }
        public int? AssignedEmployeeId { get; set; }

        public string AssignedEmployeeName { get; set; }
    }
    public class GetAllTaskInput
    {
        public TaskState? State { get; set; }
    }
    [AutoMapTo(typeof(Task))]
    public class CreateTaskInput
    {
        CreateTaskInput()
        {
            this.TenantId = 1;
        }
        public int? AssignedEmployeeId { get; set; }
        [MaxLength(TaskEntityConfiguration.TitleMaxLength)]
        public string Title { get; set; }
        [MaxLength(TaskEntityConfiguration.DescriptionMaxLengh)]
        public string Description { get; set; }
        public int TenantId { get; set; } = 1;
        public TaskState State { get; set; }

    }

    public class GetTaskInput
    {
        public int Id { get; set; }
    }
    [AutoMapTo(typeof(Task))]
    public class UpdateTaskInput
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
        public int? AssignedEmployeeId { get; set; }
        //public TaskState State { get; set; }
    }
    public class DeleteTaskInput
    {
        public int Id { get; set; }


    }
}
