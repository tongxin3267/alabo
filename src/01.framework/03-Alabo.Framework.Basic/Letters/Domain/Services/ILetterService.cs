using System;
using System.Collections.Generic;
using System.Text;
using Alabo.App.Share.Attach.Domain.Entities;
using Alabo.Domains.Services;
using MongoDB.Bson;

namespace Alabo.Framework.Basic.Letters.Domain.Services {

    public interface ILetterService : IService<Letter, ObjectId> {
    }
}