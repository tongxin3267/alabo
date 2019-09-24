using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Datas.UnitOfWorks;
using Alabo.Domains.Repositories;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Letters.Domain.Services {

    public class LetterService : ServiceBase<Letter, ObjectId>, ILetterService {

        public LetterService(IUnitOfWork unitOfWork, IRepository<Letter, ObjectId> repository) : base(unitOfWork, repository) {
        }
    }
}