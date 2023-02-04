﻿using Play.Domain.Whmcs.Models;

namespace Play.Application.Whmcs.Interfaces;

public interface IWhmcsSystemService
{
    /// <summary>
    ///     AddBannedIp
    ///     Adds a new IP address to the banned list.
    /// </summary>
    /// <param name="ip">Required</param>
    /// <param name="reason">Admin only reason</param>
    /// <param name="days">If passed, expires date is auto calculated. Optional</param>
    /// <param name="expires">YYYY-MM-DD HH:MM:SS. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/addbannedip/</docs>
    /// <returns>json string</returns>
    Task<string> AddBannedIp(string ip, string reason = null!, int? days = null, DateTime? expires = null);

    /// <summary>
    ///     GetActivityLog
    ///     Obtain the Activity Log that matches passed criteria
    /// </summary>
    /// <param name="limitstart">The offset for the returned log data (default: 0). Optional</param>
    /// <param name="limitnum">The number of records to return (default: 25). Optional</param>
    /// <param name="clientid">The ID of the client to obtain the log for. Optional</param>
    /// <param name="date">The date of the activity log to retrieve in localised format (eg 01/01/2016). Optional</param>
    /// <param name="user">The name of the user to retrieve the log entries for. Optional</param>
    /// <param name="description">Search the log for a specific string. Optional</param>
    /// <param name="ipaddress">The IP Address to search the activity log for. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getactivitylog/</docs>
    /// <returns>json string</returns>
    Task<List<WhmcsActivityLog>> GetActivityLog(int limitstart = 0, int limitnum = 0, int clientid = 0,
        string date = "", string user = "", string description = "", string ipaddress = "");

    /// <summary>
    ///     GetAdminUsers
    ///     Retrieve a list of administrator user accounts.
    /// </summary>
    /// <param name="roleid">An administrative role ID to filter for. Optional</param>
    /// <param name="email">An email address to filter for. Partial matching supported. Optional</param>
    /// <param name="include_disabled">Pass as true to include disabled administrator user accounts in response. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getadminusers/</docs>
    /// <returns>json string</returns>
    Task<List<WhmcsAdminUser>> GetAdminUsers(int roleid = 0, string email = "", bool include_disabled = false);

    /// <summary>
    ///     GetStaffOnline
    ///     Obtain a list of staff members currently online.
    /// </summary>
    /// <docs>https://developers.whmcs.com/api-reference/getstaffonline/</docs>
    /// <returns>json string</returns>
    Task<string> GetStaffOnline();

    /// <summary>
    ///     GetStats
    ///     Get business performance metrics and statistics.
    /// </summary>
    /// <param name="timeline_days">The number of days to include in the timeline data max(90). Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/getstats/</docs>
    /// <returns>json string</returns>
    Task<string> GetStats(int timeline_days = 0);

    /// <summary>
    ///     LogActivity
    ///     Create a new activity log entry.
    /// </summary>
    /// <param name="description">The description of the activity log entry</param>
    /// <param name="clientid">The client ID to associate the activity log entry with. Optional</param>
    /// <docs>https://developers.whmcs.com/api-reference/logactivity/</docs>
    /// <returns>json string</returns>
    Task<string> LogActivity(string description, int clientid = 0);
}