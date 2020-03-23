using System.Linq;
using System.Net;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using VirusTotalNet;
using VirusTotalNet.Results;
using VirusTotalNet.ResponseCodes;

using CodingExam.Helpers;
using CodingExam.Models;
using CodingExam.Common;
using System;

namespace CodingExam.HttpClients
{
    public class VirusTotalClient : IVirusTotalClient
    {
        private readonly ILogger logger;

        public VirusTotalClient(ILogger<ApiClient> nlogger)
        {
            logger = nlogger;
        }

        public async Task<ApiResponse> GetAsync(string host)
        {
            ApiResponse apiResponse = null;
            UrlScanResult urlResult = null;
            UrlReport scanReport = null;

            try
            {
                VirusTotal virusTotal = new VirusTotal(ApiSettingHelper.ApiKey);
                virusTotal.UseTLS = true;

                urlResult = await virusTotal.ScanUrlAsync(host);

                if (urlResult.ResponseCode == UrlScanResponseCode.Queued)
                {
                    scanReport = await virusTotal.GetUrlReportAsync(host);
                    VirusTotalReport report = new VirusTotalReport()
                    {
                        Host = host,
                        ScanId = scanReport.ScanId,
                        ScanDate = scanReport.ScanDate,
                        Positives = scanReport.Positives,
                        Resource = scanReport.Resource,
                        Total = scanReport.Total
                    };

                    if (scanReport.ResponseCode == UrlReportResponseCode.Present)
                    {
                        scanReport.Scans.ToList().ForEach(scan =>
                        {
                            report.ReportDetails.Add(new ReportDetail()
                            {
                                Name = scan.Key,
                                Detail = scan.Value.Detail,
                                Detected = scan.Value.Detected,
                                Result = scan.Value.Result
                            });
                        });

                        apiResponse = new ApiResponse()
                        {
                            StatusCode = HttpStatusCode.OK,
                            Result = JObject.Parse(JsonConvert.SerializeObject(report))
                        };
                    }
                    else
                    {
                        apiResponse = new ApiResponse()
                        {
                            StatusCode = HttpStatusCode.BadRequest,
                            ErrorMessage = scanReport.VerboseMsg
                        };
                    }
                }
                else
                {
                    apiResponse = new ApiResponse()
                    {
                        StatusCode = HttpStatusCode.BadRequest,
                        ErrorMessage = urlResult.VerboseMsg
                    };
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred calling external API for '{lookup}'", host);

                apiResponse = new ApiResponse()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ErrorMessage = ex.Message,
                };
            }

            return apiResponse;
        }
    }
}
