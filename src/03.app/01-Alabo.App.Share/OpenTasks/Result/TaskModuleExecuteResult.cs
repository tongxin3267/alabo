﻿using Alabo.App.Share.TaskExecutes;
using Alabo.Data.Things.Orders.Extensions;
using Alabo.Data.Things.Orders.ResultModel;
using Alabo.Framework.Tasks.Queues.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Alabo.App.Share.OpenTasks.Result
{
    public class TaskModuleExecuteResult : ITaskResult
    {
        private readonly ITaskActuator _taskActuator;

        private readonly TaskManager _taskManager;

        public TaskModuleExecuteResult(TaskContext context)
        {
            Context = context;
            _taskActuator = Context
                .HttpContextAccessor
                .HttpContext
                .RequestServices
                .GetService<ITaskActuator>();
            _taskManager = Context
                .HttpContextAccessor
                .HttpContext
                .RequestServices
                .GetService<TaskManager>();
        }

        public Guid ModuleId { get; set; }

        public TaskParameter Parameter { get; } = new TaskParameter();
        public TaskContext Context { get; }

        public ExecuteResult Update()
        {
            try
            {
                if (_taskActuator == null) {
                    return ExecuteResult.Cancel("task actuator is null.");
                }

                if (_taskManager == null) {
                    return ExecuteResult.Cancel("task manager is null.");
                }

                if (ModuleId == Guid.Empty) {
                    return ExecuteResult.Cancel("module is is empty, return.");
                }

                if (!_taskManager.ContainsModuleId(ModuleId)) {
                    return ExecuteResult.Fail($"module with id {ModuleId} not found.");
                }
                // 注释可能出现出错
                // _taskActuator.ExecuteTaskAndUpdateResults(ModuleId, Parameter);
                return ExecuteResult.Success();
            }
            catch (Exception e)
            {
                return ExecuteResult.Error(e);
            }
        }
    }
}