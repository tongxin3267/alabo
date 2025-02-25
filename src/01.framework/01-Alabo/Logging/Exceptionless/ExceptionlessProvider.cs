﻿using Alabo.Extensions;
using Alabo.Logging.Abstractions;
using Alabo.Logging.Contents;
using Alabo.Logging.NLog;
using Exceptionless;
using Microsoft.Extensions.Logging;
using System.Linq;
using NLogs = NLog;

namespace Alabo.Logging.Exceptionless {

    /// <summary>
    ///     Exceptionless日志提供程序
    /// </summary>
    public class ExceptionlessProvider : ILogProvider {

        /// <summary>
        ///     客户端
        /// </summary>
        private readonly ExceptionlessClient _client;

        /// <summary>
        ///     NLog日志操作，用于控制日志级别是否启用
        /// </summary>
        private readonly NLogs.ILogger _logger;

        /// <summary>
        ///     行号
        /// </summary>
        private int _line;

        /// <summary>
        ///     初始化Exceptionless日志提供程序
        /// </summary>
        /// <param name="logName">日志名称</param>
        public ExceptionlessProvider(string logName) {
            _logger = NLogProvider.GetLogger(logName);
            _client = ExceptionlessClient.Default;
        }

        /// <summary>
        ///     日志名称
        /// </summary>
        public string LogName => _logger.Name;

        /// <summary>
        ///     调试级别是否启用
        /// </summary>
        public bool IsDebugEnabled => _logger.IsDebugEnabled;

        /// <summary>
        ///     跟踪级别是否启用
        /// </summary>
        public bool IsTraceEnabled => _logger.IsTraceEnabled;

        /// <summary>
        ///     写日志
        /// </summary>
        /// <param name="level">日志等级</param>
        /// <param name="content">日志内容</param>
        public void WriteLog(LogLevel level, ILogContent content) {
            InitLine();
            var builder = CreateBuilder(level, content);
            SetUser(content);
            SetSource(builder, content);
            SetReferenceId(builder, content);
            AddProperties(builder, content as ILogConvert);
            builder.Submit();
        }

        /// <summary>
        ///     初始化行号
        /// </summary>
        private void InitLine() {
            _line = 1;
        }

        /// <summary>
        ///     创建事件生成器
        /// </summary>
        private EventBuilder CreateBuilder(LogLevel level, ILogContent content) {
            if (content.Exception != null) {
                return _client.CreateException(content.Exception);
            }

            return _client.CreateLog(GetMessage(content), ConvertTo(level));
        }

        /// <summary>
        ///     获取日志消息
        /// </summary>
        /// <param name="content">日志内容</param>
        private string GetMessage(ILogContent content) {
            if (content is ICaption caption && string.IsNullOrWhiteSpace(caption.Caption) == false) {
                return caption.Caption;
            }

            if (content.Content.Length > 0) {
                return content.Content.ToString();
            }

            return content.TraceId;
        }

        /// <summary>
        ///     转换日志等级
        /// </summary>
        private global::Exceptionless.Logging.LogLevel ConvertTo(LogLevel level) {
            switch (level) {
                case LogLevel.Trace:
                    return global::Exceptionless.Logging.LogLevel.Trace;

                case LogLevel.Debug:
                    return global::Exceptionless.Logging.LogLevel.Debug;

                case LogLevel.Information:
                    return global::Exceptionless.Logging.LogLevel.Info;

                case LogLevel.Warning:
                    return global::Exceptionless.Logging.LogLevel.Warn;

                case LogLevel.Error:
                    return global::Exceptionless.Logging.LogLevel.Error;

                case LogLevel.Critical:
                    return global::Exceptionless.Logging.LogLevel.Fatal;

                default:
                    return global::Exceptionless.Logging.LogLevel.Off;
            }
        }

        /// <summary>
        ///     设置用户信息
        /// </summary>
        private void SetUser(ILogContent content) {
            if (string.IsNullOrWhiteSpace(content.UserId)) {
                return;
            }

            _client.Configuration.SetUserIdentity(content.UserId);
        }

        /// <summary>
        ///     设置来源
        /// </summary>
        private void SetSource(EventBuilder builder, ILogContent content) {
            if (string.IsNullOrWhiteSpace(content.Url)) {
                return;
            }

            builder.SetSource(content.Url);
        }

        /// <summary>
        ///     设置跟踪号
        /// </summary>
        private void SetReferenceId(EventBuilder builder, ILogContent content) {
            builder.SetReferenceId(content.TraceId);
        }

        /// <summary>
        ///     添加属性集合
        /// </summary>
        private void AddProperties(EventBuilder builder, ILogConvert content) {
            if (content == null) {
                return;
            }

            foreach (var parameter in content.To().OrderBy(t => t.SortId)) {
                if (string.IsNullOrWhiteSpace(parameter.Value.SafeString())) {
                    continue;
                }

                builder.SetProperty($"{GetLine()}. {parameter.Text}", parameter.Value);
            }
        }

        /// <summary>
        ///     获取行号
        /// </summary>
        private string GetLine() {
            return _line++.ToString().PadLeft(2, '0');
        }
    }
}