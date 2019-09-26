using System;
using System.Collections.Generic;
using System.Data;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Repositories.EFCore;
using Alabo.Domains.Repositories.Extensions;
using ZKCloud.Open.Message.Models;
using MessageQueue = Alabo.Framework.Basic.Notifications.Domain.Entities.MessageQueue;

namespace Alabo.Framework.Basic.Notifications.Domain.Repositories {

    public class MessageQueueRepository : RepositoryEfCore<MessageQueue, long>, IMessageQueueRepository {

        public MessageQueueRepository(IUnitOfWork unitOfWork) : base(unitOfWork) {
        }

        public void Add(MessageQueue entity) {
            if (entity == null) {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity.Message == null) {
                entity.Message = string.Empty;
            }

            if (entity.Summary == null) {
                entity.Summary = string.Empty;
            }

            if (entity.Parameters == null) {
                entity.Parameters = string.Empty;
            }

            var sql =
                "insert into Basic_MessageQueue(TemplateCode, Mobile, Content, [Parameters], Status, Message, Summary, RequestTime, SendTime,IpAdress) values(@templatecode, @mobilse, @contentt, @parameters, @status, N'', N'', GETDATE(), GETDATE(),@ipadress); select @@identity;";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@templatecode", entity.TemplateCode),
                RepositoryContext.CreateParameter("@mobilse", entity.Mobile),
                RepositoryContext.CreateParameter("@contentt", entity.Content),
                RepositoryContext.CreateParameter("@parameters", entity.Parameters),
                RepositoryContext.CreateParameter("@status", entity.Status),
                RepositoryContext.CreateParameter("@ipadress", entity.IpAdress)
            };
            var result = RepositoryContext.ExecuteScalar(sql, parameters);
            if (result != null && result != DBNull.Value) {
                entity.Id = Convert.ToInt64(result);
            }
        }

        public void Cancel(long id) {
            var sql = $"update Basic_MessageQueue set Status={(byte)MessageStatus.Canceld} where Id={id}";
            RepositoryContext.ExecuteNonQuery(sql);
        }

        public void ErrorQueue(long id, string message) {
            var sql =
                "update Basic_MessageQueue set Status=@status, Message=@message,RequestTime=GETDATE() where Id=@id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@status", MessageStatus.Error),
                RepositoryContext.CreateParameter("@message", message),
                RepositoryContext.CreateParameter("@id", id)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        public void ErrorQueue(long id, string message, string summary) {
            var sql =
                "update Basic_MessageQueue set Status=@status, Message=@message, Summary=@summary,RequestTime=GETDATE() where Id=@id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@status", MessageStatus.Error),
                RepositoryContext.CreateParameter("@message", message),
                RepositoryContext.CreateParameter("@summary", summary),
                RepositoryContext.CreateParameter("@id", id)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        public void ErrorQueue(long id, string message, Exception exception) {
            var sql =
                "update Basic_MessageQueue set Status=@status, Message=@message, Summary=@summary,RequestTime=GETDATE() where Id=@id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@status", MessageStatus.Error),
                RepositoryContext.CreateParameter("@message", message),
                RepositoryContext.CreateParameter("@summary", exception.ToString()),
                RepositoryContext.CreateParameter("@id", id)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        public void HandleQueue(long id, string message = null, string summary = null) {
            if (message == null) {
                message = string.Empty;
            }

            if (summary == null) {
                summary = string.Empty;
            }

            var sql =
                "update Basic_MessageQueue set Status=@status, Message=@message, Summary=@summary,RequestTime=GETDATE() where Id=@id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@status", MessageStatus.Handled),
                RepositoryContext.CreateParameter("@message", message),
                RepositoryContext.CreateParameter("@summary", summary),
                RepositoryContext.CreateParameter("@id", id)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        public void HandleQueueAndUpdateContent(long id, string message = null, string summary = null) {
            if (message == null) {
                message = string.Empty;
            }

            if (summary == null) {
                summary = string.Empty;
            }

            var sql =
                "update Basic_MessageQueue set Status=@status,  Message=@message, Summary=@summary,RequestTime=GETDATE() where Id=@id";
            var parameters = new[]
            {
                RepositoryContext.CreateParameter("@status", MessageStatus.Handled),
                RepositoryContext.CreateParameter("@message", message),
                RepositoryContext.CreateParameter("@summary", summary),
                RepositoryContext.CreateParameter("@id", id)
            };
            RepositoryContext.ExecuteNonQuery(sql, parameters);
        }

        public MessageQueue GetSingle(long id) {
            var sql = $"select * from Basic_MessageQueue where Id={id}";
            MessageQueue result = null;
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                if (dr.Read()) {
                    result = ReadQueue(dr);
                }
            }

            return result;
        }

        public IList<long> GetUnHandledIdList() {
            var sql = $"select  Id from Basic_MessageQueue where Status={(byte)MessageStatus.Pending} order by id  ";
            IList<long> list = new List<long>();
            using (var dr = RepositoryContext.ExecuteDataReader(sql)) {
                while (dr.Read()) {
                    list.Add(dr.Read<long>("Id"));
                }
            }

            return list;
        }

        private MessageQueue ReadQueue(IDataReader dr) {
            var result = new MessageQueue {
                Id = dr.Read<long>("Id"),
                Content = dr.Read<string>("Content"),
                Message = dr.Read<string>("Message"),
                Mobile = dr.Read<string>("Mobile"),
                Parameters = dr.Read<string>("Parameters"),
                RequestTime = dr.Read<DateTime>("RequestTime"),
                SendTime = dr.Read<DateTime>("SendTime"),
                Status = (MessageStatus)dr.Read<byte>("Status"),
                Summary = dr.Read<string>("Summary"),
                IpAdress = dr.Read<string>("IpAdress"),
                TemplateCode = dr.Read<long>("TemplateCode")
            };
            return result;
        }
    }
}