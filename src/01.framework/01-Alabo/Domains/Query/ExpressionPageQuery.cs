using Alabo.Domains.Entities.Core;
using System.Linq;

namespace Alabo.Domains.Query
{
    internal class ExpressionPageQuery<TEntity> : ExpressionOrderQuery<TEntity>, IPageQuery<TEntity>
        where TEntity : class, IEntity
    {
        public bool EnablePaging { get; set; } = false;

        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 20;

        public override IQueryable<TEntity> Execute(IQueryable<TEntity> query)
        {
            query = base.Execute(query);
            if (!EnablePaging) {
                return query;
            }

            if (PageSize <= 0) {
                PageSize = 20;
            }

            if (PageIndex <= 0) {
                PageIndex = 1;
            }

            return query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        }

        public int ExecuteCountQuery(IQueryable<TEntity> query)
        {
            return base.Execute(query).Count();
        }
    }
}