using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TRIP.Models
{
    public class tourinfo
    {
        public int idx {  get; set; }
        public string name { get; set; }
        public string category {  get; set; }
        public string area {  get; set; }
        public string copy {  get; set; }
        public string manage {  get; set; }
        public string phone { get; set; }
        public string homepage {  get; set; }
        public string content {  get; set; }
        public string fee { get; set; }
        public string usehour {  get; set; }
        public string address {  get; set; }
        public double xposition {  get; set; }
        public double yposition { get; set; }
        public string parking { get; set; }
        public string images { get; set; }

        public static readonly string INSERT_QUERY = @"INSERT INTO [dbo].[tourinfo]
                                                                   ([Id]
                                                                   ,[idx]
                                                                   ,[name]
                                                                   ,[category]
                                                                   ,[area]
                                                                   ,[copy]
                                                                   ,[phone]
                                                                   ,[homepage]
                                                                   ,[content]
                                                                   ,[fee]
                                                                   ,[usehour]
                                                                   ,[address]
                                                                   ,[xposition]
                                                                   ,[yposition]
                                                                   ,[images])
                                                             VALUES
                                                                   (@Id
                                                                   ,@idx
                                                                   ,@name
                                                                   ,@category
                                                                   ,@area
                                                                   ,@copy
                                                                   ,@phone
                                                                   ,@homepage
                                                                   ,@content
                                                                   ,@fee
                                                                   ,@usehour
                                                                   ,@address
                                                                   ,@xposition
                                                                   ,@yposition
                                                                   ,@images)";
        public static readonly string SELECT_QUERY = @"SELECT [Id]
                                                              ,[idx]
                                                              ,[name]
                                                              ,[category]
                                                              ,[area]
                                                              ,[copy]
                                                              ,[phone]
                                                              ,[homepage]
                                                              ,[content]
                                                              ,[fee]
                                                              ,[usehour]
                                                              ,[address]
                                                              ,[xposition]
                                                              ,[yposition]
                                                              ,[images]
                                                          FROM [dbo].[tourinfo]";
        public static readonly string GETDATE_QUERY = @"SELECT CONVERT(CHAR(10), Timestamp, 23) AS Save_Date
                                                          FROM [dbo].[tourinfo]
                                                         GROUP BY CONVERT(CHAR(10), Timestamp, 23)";

    }
}
