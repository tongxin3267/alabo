﻿using Microsoft.Extensions.DependencyInjection;
using System;
using Alabo.App.Core.Tasks;
using Alabo.App.Core.Tasks.Extensions;
using Alabo.App.Core.Tasks.ResultModel;

namespace Alabo.App.Share.Tasks.Result {

    public class TaskModuleExecuteResult : ITaskResult {
        public TaskContext Context { get; private set; }

        public Guid ModuleId { get; set; }

        public TaskParameter Parameter { get; private set; } = new TaskParameter();

        private readonly ITaskActuator _taskActuator;

        private TaskManager _taskManager;

        public TaskModuleExecuteResult(TaskContext context) {
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

        public ExecuteResult Update() {
            try {
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
            } catch (Exception e) {
                return ExecuteResult.Error(e);
            }
        }
    }
}