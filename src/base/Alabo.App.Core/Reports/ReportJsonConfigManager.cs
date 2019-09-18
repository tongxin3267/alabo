using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Alabo.Extensions;
using Alabo.Runtime;

namespace Alabo.App.Core.Reports {

    public class ReportJsonConfigManager {

        private static readonly string _jsonReportConfigRootPath =
            Path.Combine(RuntimeContext.Current.Path.AppDataDirectory, "reports");

        private readonly ILogger<ReportJsonConfigManager> _logger;

        private readonly IDictionary<Guid, ReportRuleConfig> _reportRuleDictionary;

        public ReportJsonConfigManager(ILoggerFactory loggerFactory) {
            _logger = loggerFactory.CreateLogger<ReportJsonConfigManager>();
            var reportRuleList = GetReportRuleConfigs(_jsonReportConfigRootPath);
            _reportRuleDictionary = reportRuleList.ToDictionary(e => e.Id, e => e);
        }

        private IEnumerable<ReportRuleConfig> GetReportRuleConfigs(string path) {
            if (!Directory.Exists(path)) {
                throw new DirectoryNotFoundException($"directory {path} not found.");
            }

            var currentDirectory = new DirectoryInfo(path);
            IList<ReportRuleConfig> list = new List<ReportRuleConfig>();
            var files = currentDirectory.GetFiles("*.json");
            foreach (var item in files) {
                try {
                    list.Add(ReadRuleConfig(item.FullName));
                } catch (Exception e) {
                    _logger.LogError($"read report rule config error, path:{path}, message:{e}");
                }
            }

            var directories = currentDirectory.GetDirectories();
            foreach (var item in directories) {
                list.AddRange(GetReportRuleConfigs(item.FullName));
            }

            return list;
        }

        private ReportRuleConfig ReadRuleConfig(string path) {
            if (!File.Exists(path)) {
                throw new FileNotFoundException($"file with path {path} not found.");
            }

            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)) {
                var sr = new StreamReader(fs, Encoding.UTF8);
                return JsonConvert.DeserializeObject<ReportRuleConfig>(sr.ReadToEnd());
            }
        }

        public ReportRuleConfig GetRuleConfig(Guid id) {
            if (!_reportRuleDictionary.TryGetValue(id, out var result)) {
                throw new ReportRuleNotFoundException($"report rule with id {id} not found.");
            }

            return result;
        }

        public bool HasRule(Guid id) {
            return _reportRuleDictionary.ContainsKey(id);
        }
    }
}