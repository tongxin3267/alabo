namespace Alabo.App.Core.ApiStore.MiniProgram.Dtos {

    /// <summary>
    ///     微信错误返回格式 {"errcode":40029,"errmsg":"invalid code, hints: [ req_id: Hs2Q7a0732th50 ]"}
    /// </summary>
    public class MiniErrorMessage {
        public string Errcode { get; set; }

        public string Errmsg { get; set; }
    }
}