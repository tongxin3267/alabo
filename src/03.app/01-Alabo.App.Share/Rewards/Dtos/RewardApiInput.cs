using Alabo.Domains.Query.Dto;

namespace Alabo.App.Share.Rewards.Dtos
{
    /// <summary>
    ///     Class RewardApiInput.
    /// </summary>
    /// <seealso cref="Alabo.Domains.Query.Dto.ApiInputDto" />
    public class RewardApiInput : ApiInputDto
    {
        /// <summary>
        ///     分润用户ID，获得收益的用户
        /// </summary>
        /// <value>The user identifier.</value>
        public long UserId { get; set; }
    }
}