/********************************************************************
	created:	16:10:2017   15:35
	author:		jordenwu
	purpose:	Http服务类
*********************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using UnityEngine;
using JW.Common;
using System.Collections;
using UnityEngine.Networking;

namespace JW.Framework.Http
{
    public static class HttpHelper
    {

        private static readonly string DefaultUserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

        public static IEnumerator WWWRequest(string kURL, System.Action<string> kCallBack)
        {
            WWW www = new WWW(kURL);
            
            yield return www;
            if (www.error != null)
            {
                JW.Common.Log.LogE("WWW Request error,WWW URL = " + kURL);
                JW.Common.Log.LogE(www.error);
                if (kCallBack != null)
                    kCallBack("error");
            }
            else
            {
                if (kCallBack != null)
                    kCallBack(www.text);
            }
        }

        public static IEnumerator WWWRequest(string kURL, WWWForm kParams, System.Action<string> kCallBack)
        {
            WWW www = new WWW(kURL, kParams);
            yield return www;

            if (www.error != null)
            {
                JW.Common.Log.LogE("WWW Request error,WWW URL = " + kURL);
                JW.Common.Log.LogE(www.error);
                if (kCallBack != null)
                    kCallBack("error");
            }
            else
            {
                if (kCallBack != null)
                    kCallBack(www.text);
            }
        }

        /// 创建GET方式的HTTP请求  
        /// <param name="url">请求的URL</param>  
        /// <param name="timeout">请求的超时时间(秒)</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static string SyncGetHttpResponse(string url, int timeout = 10, string userAgent = null, CookieCollection cookies = null)
        {
            string kResult = string.Empty;
            HttpWebResponse kResponse = null;
            if (string.IsNullOrEmpty(url))
            {
                JW.Common.Log.LogE("无效的URL:" + url);
                return kResult;
            }
            try
            {
                HttpWebRequest kRequest = WebRequest.Create(url) as HttpWebRequest;
                //如果是发送HTTPS请求  
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                    kRequest.ProtocolVersion = HttpVersion.Version10;
                }

                kRequest.Method = "GET";
                kRequest.UserAgent = DefaultUserAgent;
                if (!string.IsNullOrEmpty(userAgent))
                {
                    kRequest.UserAgent = userAgent;
                }
                kRequest.Timeout = timeout * 1000;
                //
                if (cookies != null)
                {
                    kRequest.CookieContainer = new CookieContainer();
                    kRequest.CookieContainer.Add(cookies);
                }
                kResponse = (HttpWebResponse)kRequest.GetResponse();
                Stream kResponseStream = kResponse.GetResponseStream();
                if (kResponseStream != null)
                {
                    StreamReader reader = new StreamReader(kResponseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    kResult = reader.ReadToEnd();
                    reader.Close();
                    kResponseStream.Close();
                    kRequest.Abort();
                    kResponse.Close();
                    kResult = kResult.Trim();
                }
            }
            catch (System.Exception e)
            {
                JW.Common.Log.LogE("-SyncGetHttpResponse--> " + e.ToString());
            }
            finally
            {
                if (kResponse != null)
                    kResponse.Close();
                kResponse = null;
            }
            return kResult;
        }


        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="parameters">随同请求POST的参数名称及参数值字典</param>  
        /// <param name="timeout">请求的超时时间</param>  
        /// <param name="userAgent">请求的客户端浏览器信息，可以为空</param>  
        /// <param name="requestEncoding">发送HTTP请求时所用的编码</param>  
        /// <param name="cookies">随同HTTP请求发送的Cookie信息，如果不需要身份验证可以为空</param>  
        /// <returns></returns>  
        public static HttpWebResponse SyncPostHttpResponse(string url, IDictionary<string, string> parameters, int? timeout, string userAgent, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                JW.Common.Log.LogE("无效的URL:" + url);
                return null;
            }
            if (requestEncoding == null)
            {
                JW.Common.Log.LogE("requestEncoding 不能为空");
                return null;
            }

            HttpWebRequest kRequest = null;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                kRequest = WebRequest.Create(url) as HttpWebRequest;
                kRequest.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                kRequest = WebRequest.Create(url) as HttpWebRequest;
            }
            kRequest.Method = "POST";
            kRequest.ContentType = "application/x-www-form-urlencoded";

            if (!string.IsNullOrEmpty(userAgent))
            {
                kRequest.UserAgent = userAgent;
            }
            else
            {
                kRequest.UserAgent = DefaultUserAgent;
            }

            if (timeout.HasValue)
            {
                kRequest.Timeout = timeout.Value * 1000;
            }
            if (cookies != null)
            {
                kRequest.CookieContainer = new CookieContainer();
                kRequest.CookieContainer.Add(cookies);
            }
            //如果需要POST数据  
            if (!(parameters == null || parameters.Count == 0))
            {
                StringBuilder buffer = new StringBuilder();
                int i = 0;
                foreach (string key in parameters.Keys)
                {
                    if (i > 0)
                    {
                        buffer.AppendFormat("&{0}={1}", key, parameters[key]);
                    }
                    else
                    {
                        buffer.AppendFormat("{0}={1}", key, parameters[key]);
                    }
                    i++;
                }
                byte[] bytes = requestEncoding.GetBytes(buffer.ToString());
                using (Stream kStream = kRequest.GetRequestStream())
                {
                    kStream.Write(bytes, 0, bytes.Length);
                    kStream.Close();
                }
            }
            return kRequest.GetResponse() as HttpWebResponse;
        }

        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受  
        }
        
        /// <summary>  
        /// 创建POST方式的HTTP请求  
        /// </summary>  
        /// <param name="url">请求的URL</param>  
        /// <param name="postData">随同请求POST的参数json字符串</param>  
        /// <param name="kCallBack">请求的回调，返回response文本</param>  
        /// <returns></returns>  
        public static IEnumerator GetTextByPostUrl(string url, string postData, System.Action<string> kCallBack)
        {
            using (UnityWebRequest www = new UnityWebRequest(url,"POST"))
            {
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(postData);
                www.uploadHandler = (UploadHandler)new UploadHandlerRaw(postBytes);
                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
                www.SetRequestHeader("Content-Type", "application/json");
                www.timeout = 10;
                yield return www.SendWebRequest();
                if (www.isNetworkError||www.isHttpError)
                {
                    JW.Common.Log.LogE("HTTP GetTextByPostUrl Net Error:{0}", url);
                    JW.Common.Log.LogE(www.error);
                    kCallBack("error");
                }
                else
                {
                    // Show results as text  
                    if (www.responseCode == 200)
                    {
                        kCallBack(www.downloadHandler.text);
                    }
                    else
                    {
                        JW.Common.Log.LogE(www.error);
                        kCallBack("error");
                    }
                }
            }
        }
    }
}
