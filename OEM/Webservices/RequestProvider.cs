using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OEM.Webservices.Dtos;
using Plugin.Connectivity;
using Polly;
using Polly.Timeout;
using Polly.Wrap;
using Refit;

namespace OEM.Webservices
{
    public class RequestProvider
    {
        public AsyncPolicyWrap GetDefaultPolicyWithRetries(int numberOfRetries)
        {
            var timeoutPolicy = Policy.TimeoutAsync(seconds: 10, timeoutStrategy: TimeoutStrategy.Pessimistic);

            var retryPolicy = Policy.Handle<WebException>()
                .Or<HttpRequestException>()
                .Or<TimeoutException>()
                .Or<Exception>()
                .WaitAndRetryAsync
                (
                    retryCount: numberOfRetries,
                    sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                     (exception, timeSpan, retries, context) =>
                     {
                         if (numberOfRetries != retries)
                             return;
                             // only log if the final retry fails

                         }
                );

            return Policy.WrapAsync(retryPolicy, timeoutPolicy); // demonstrates alternative PolicyWrap syntax
        }

        public async Task<T> MakeRequestAsync<T>(Task<T> request, AsyncPolicyWrap policy) where T : Status
        {
            T response = (T)Activator.CreateInstance(typeof(T));
            if (!CrossConnectivity.Current.IsConnected)
            {
                response.ErrorCode = "";
                response.ErrorMessage = ErrorMessageConstants.DEVICE_CONNECTIVITY_ERROR;
                response.IsSuccess = false;
            }
            else
            {
                try
                {
                    response = await policy.ExecuteAsync(async () => await request);
                    response.IsSuccess = true;
                }
                catch (AggregateException e)
                {
                    foreach (var nestedException in e.InnerExceptions)
                    {
                        response.ErrorCode = "";
                        response.ErrorMessage = nestedException.Message;
                        response.IsSuccess = false;
                    }
                }
                catch (ApiException e)
                {
                    response.ErrorCode = e.StatusCode.ToString();
                    response.ErrorMessage = e.Message;
                    response.IsSuccess = false;
                }
                catch (Exception e)
                {
                    response.ErrorCode = "";
                    response.ErrorMessage = e.Message;
                    response.IsSuccess = false;
                }
            }

            return response;
        }

    }
}
