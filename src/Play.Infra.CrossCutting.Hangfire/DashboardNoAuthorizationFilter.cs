using System.Diagnostics;
using Hangfire.Dashboard;

namespace Play.Infra.CrossCutting.Hangfire;

/// <summary>
///     This is a custom authorization filter for the Hangfire dashboard framework.
///     It allows or denies access to the dashboard depending on the value of the Cf-Connecting-Ip HTTP header.
///     If the header is present and its value is 62.169.248.48, access is granted. If the header is not present or its
///     value is different, access is denied.
/// </summary>
public class DashboardNoAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext dashboardContext)
    {
        //check if we are in debug mode
        if (Debugger.IsAttached) return true;

        //if header contains the Cf-Connecting-Ip key with the value 62.169.248.48 then allow access
        return dashboardContext.GetHttpContext().Request.Headers["Cf-Connecting-Ip"] == "62.169.248.48";
    }
}