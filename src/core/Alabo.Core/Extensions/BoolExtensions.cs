namespace Alabo.Core.Extensions
{
    public static class BoolExtensions
    {
        public static string GetHtmlName(this bool value)
        {
            var result = string.Empty;
            if (value) {
                result = @"<span class='m-badge m-badge--wide  m-badge--success'>是</span>";
            } else {
                result = @"<span class='m-badge m-badge--wide m-badge--danger'>否</span>";
            }

            return result;
        }

        public static string GetDisplayName(this bool value)
        {
            var result = string.Empty;
            if (value) {
                result = @"是";
            } else {
                result = @"否";
            }

            return result;
        }
    }
}