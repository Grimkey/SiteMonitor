using System;
using System.Collections.Generic;
using System.Net.Http;

namespace SiteMonitor
{
    public class SiteCheck
    {
        public void Validate(IEnumerable<string> urlList)
        {
            var logger = SiteLogger.GetSessionLogger();

            var successful = true;
            foreach(var url in urlList)
            {
                logger.WriteTrace($"Testing url {url}");
                var result = Validate(url);

                if (!result.successful)
                {
                    successful = false;
                    logger.WriteError(result.message, url, result.exception);
                }
            }

            if (successful)
            {
                logger.WriteSuccess("All URLs passed.");
            }
        }

        private (bool successful, string message, Exception exception) Validate(string url)
        {
            var httpClient = new HttpClient();

            var response = (HttpResponseMessage)null;
            try
            {
                response = httpClient.GetAsync(url).Result;
            }
            catch(Exception err)
            {
                return (false, "Exception occurred calling HttpClient::GetAsync", err);
            }

            if (!response.IsSuccessStatusCode) {
                return (false, $"Failure status code: {response.StatusCode}", null);
            }

            return (true, string.Empty, null);
        }
    }
}
