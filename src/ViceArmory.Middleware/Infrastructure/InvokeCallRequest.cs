using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using ViceArmory.DTO.ResponseObject.Authenticate;
using ViceArmory.RequestObject.Product;
using ViceArmory.Utility;
using static ViceArmory.Utility.CommonEnums;

namespace Middleware.Infrastructure
{
    public class InvokeCallRequest
    {
        private AuthenticateResponse _UserInfo;
        /// <summary>
        /// Method to make api calls as per the method name.
        /// </summary>
        /// <param name="methodName">name of api method to be called</param>
        /// <returns>json string to be parsed as desired object</returns>
        public static string GetResponseString(string methodName, string aPIUrl)
        {
            HttpClientHandler handler = new HttpClientHandler() // inclulde compression in request.
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            using (var client = new HttpClient(handler))
            {
                string resultString = string.Empty;
                try
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(aPIUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Method to set token
                    string userId = string.Empty;

                    string webApiPath = GetWebApiPath(methodName);

                    //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                    HttpResponseMessage response = client.GetAsync(webApiPath).Result;

                    //Checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        resultString = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                        //var obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                        //object resultObject = Convert.ToString(response.Content.ReadAsStringAsync().Result)

                        //resultString = Convert.ToString(resultObject);
                    }

                    return resultString;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// common post request to get response from web api for web project.
        /// </summary>
        /// <param name="methodName">method of web api to be called</param>
        /// <param name="objRequestModel">request model to be passed</param>
        /// <returns>string</returns>
        public static string PostRequestGetResponseString(string methodName, object objRequestModel, string aPIUrl)
        {
            HttpClientHandler handler = new HttpClientHandler() // inclulde compression in request.
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            //using (var client = new HttpClient())
            //LoggingHandler.Write("PostRequestGetResponseString - start", LoggingLevel.DEBUG);
            using (var client = new HttpClient(handler))
            {
                string resultString = string.Empty;
                try
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(aPIUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                    //if (objSecureAuth != null)
                    //{
                    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objSecureAuth.TokenId);
                    //}

                    // Method to set token
                    string userId = string.Empty;

                    string webApiPath = GetWebApiPath(methodName);

                    //LoggingHandler.Write("webApiPath" + webApiPath, LoggingLevel.DEBUG);
                    //Sending request to find web api REST service resource using HttpClient  
                    HttpResponseMessage response = client.PostAsJsonAsync(webApiPath, objRequestModel).Result;
                    //LoggingHandler.Write("response = " + response, LoggingLevel.DEBUG);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        //var obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        //object resultObject = (object)obj.SelectToken("Result");

                        resultString = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                    }
                    return resultString;
                }
                catch (Exception ex)
                {
                    //LoggingHandler.Write("Exception =  " + ex.InnerException + "Message = " + ex.Message, LoggingLevel.ERROR);
                    throw ex;

                }
            }
        }

        public static string PostRequestGetResponseString(string methodName, object objRequestModel, string aPIUrl, AuthenticateResponse objSecureAuth)
        {
            HttpClientHandler handler = new HttpClientHandler() // inclulde compression in request.
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            //using (var client = new HttpClient())
            //LoggingHandler.Write("PostRequestGetResponseString - start", LoggingLevel.DEBUG);
            using (var client = new HttpClient(handler))
            {
                string resultString = string.Empty;
                try
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(aPIUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                    if (objSecureAuth != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objSecureAuth.TokenId);
                    }

                    // Method to set token
                    string userId = string.Empty;

                    string webApiPath = GetWebApiPath(methodName);

                    //LoggingHandler.Write("webApiPath" + webApiPath, LoggingLevel.DEBUG);
                    //Sending request to find web api REST service resource using HttpClient  
                    HttpResponseMessage response = client.PostAsJsonAsync(webApiPath, objRequestModel).Result;
                    //LoggingHandler.Write("response = " + response, LoggingLevel.DEBUG);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        //var obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        //object resultObject = (object)obj.SelectToken("Result");

                        resultString = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                    }
                    return resultString;
                }
                catch (Exception ex)
                {
                    //LoggingHandler.Write("Exception =  " + ex.InnerException + "Message = " + ex.Message, LoggingLevel.ERROR);
                    throw ex;

                }
            }
        }
        /// <summary>
        /// Gets formatted web api path to be called for web.
        /// </summary>
        /// <param name="methodName">api method name</param>
        /// <returns>string</returns>
        public static string GetWebApiPath(string methodName)
        {
            string webApiPath = string.Format("{0}/{1}/{2}", Constants.API_CAPTION, Constants.API_VERSION, methodName);
            return webApiPath;
        }

        public static string PostFileRequestGetResponseString(string methodName, object objRequestModel, string aPIUrl, AuthenticateResponse objSecureAuth = null)
        {
            HttpClientHandler handler = new HttpClientHandler() // inclulde compression in request.
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            //using (var client = new HttpClient())
            //LoggingHandler.Write("PostRequestGetResponseString - start", LoggingLevel.DEBUG);
            using (var client = new HttpClient(handler))
            {
                string resultString = string.Empty;
                try
                {
                    //Passing service base url  
                    client.BaseAddress = new Uri(aPIUrl);

                    client.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
                    //client.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
                    if (objSecureAuth != null)
                    {
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", objSecureAuth.TokenId);
                    }

                    var requestObject = (ProductImageRequest)objRequestModel;


                    var content = new MultipartFormDataContent();
                    foreach (var item in requestObject.ProductImages)
                    {
                        //var mStream = new MemoryStream();
                        //item.CopyToAsync(mStream);
                        Stream objStream= item.OpenReadStream();
                        var fileContent = new StreamContent(objStream);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                        fileContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data")
                        {
                            Name = "ProductImages",
                            FileName = item.FileName
                        };
                        content.Add(fileContent);
                        //content.Add(new StreamContent(mStream), "productImagesReq.ProductImages", item.FileName);
                    }
                    //content.Add(new StringContent(JsonConvert.SerializeObject(requestObject), Encoding.UTF8, "application/json"));
                    content.Add(new StringContent(requestObject.ProductId), "ProductId");
                    content.Add(new StringContent(requestObject.UserId), "UserId");
                    content.Add(new StringContent(requestObject.CreatedAt.ToString()), "CreatedAt");
                    content.Add(new StringContent(requestObject.IPAddress), "IPAddress");
                    content.Add(new StringContent(requestObject.UpdatedAt.ToString()), "UpdatedAt");


                    //HttpContent content = new StringContent("fileToUpload");
                    //content.Add(objRequestModel);

                    // Method to set token
                    //string userId = string.Empty;

                    string webApiPath = GetWebApiPath(methodName);

                    HttpRequestMessage msg = new HttpRequestMessage(HttpMethod.Post, webApiPath);
                    msg.Content = content;
                    //LoggingHandler.Write("webApiPath" + webApiPath, LoggingLevel.DEBUG);
                    //Sending request to find web api REST service resource using HttpClient  
                    HttpResponseMessage response = client.SendAsync(msg).Result; //PostAsJsonAsync(webApiPath, content).Result;
                    //LoggingHandler.Write("response = " + response, LoggingLevel.DEBUG);
                    //Checking the response is successful or not which is sent using HttpClient
                    if (response.IsSuccessStatusCode)
                    {
                        //var obj = JObject.Parse(response.Content.ReadAsStringAsync().Result);
                        //object resultObject = (object)obj.SelectToken("Result");

                        resultString = Convert.ToString(response.Content.ReadAsStringAsync().Result);
                    }
                    return resultString;
                }
                catch (Exception ex)
                {
                    //LoggingHandler.Write("Exception =  " + ex.InnerException + "Message = " + ex.Message, LoggingLevel.ERROR);
                    throw ex;

                }
            }
        }

    }
}
