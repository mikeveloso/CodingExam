using System;
using System.Collections.Generic;
using System.Net;



namespace CodingExam.Models
{
    public class VirusTotalReport
    {
        public VirusTotalReport()
        {
            ReportDetails = new List<ReportDetail>();
        }

        public string Host { get; set; }
        public string ScanId { get; set; }

        public DateTime ScanDate { get; set; }

        public string Resource { get; set; }

        public int Positives { get; set; }

        public int Total { get; set; }

        public List<ReportDetail> ReportDetails { get; set; }
    }
}
