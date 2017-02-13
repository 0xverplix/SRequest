using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SRequest
{
    public class HTTPRequest
    {
        /// <summary>
        /// Make a HTTP Request
        /// </summary>
        /// <param name="URL">URL of Request</param>
        /// <param name="Host">Host of Request</param>
        /// <param name="UserAgent">UserAgent</param>
        /// <param name="ContentType">ContentType</param>
        /// <param name="Accept">Accept</param>
        /// <param name="Referer">Referer</param>
        /// <param name="Params">POST Params</param>
        /// <param name="Cookies">Cookies</param>
        /// <param name="Headers">Request Headers</param>
        /// <param name="isPost">Bool for POST or GET</param>
        /// <returns>Source of Req</returns>
        public string MakeRequest(string URL,
            string Host,
            string UserAgent,
            string ContentType,
            string Accept,
            string Referer,
            List<HTTPRequestParams> Params,
            List<HTTPRequestCookie> Cookies, 
            List<HTTPRequestHeader> Headers, 
            bool isPost
        )
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.Host = Host.ToString();
                request.KeepAlive = true;
                request.UserAgent = UserAgent.ToString();
                request.ContentType = ContentType.ToString();
                request.Accept = Accept.ToString();
                request.Referer = Referer.ToString();
                string cookies = string.Empty;
                for (int i = 0, n = Headers.Count; i < n; i++)
                    request.Headers.Add(Headers[i].Header, Headers[i].Value);

                for (int i = 0, n = Cookies.Count; i < n; i++)
                    cookies = cookies + $"{Cookies[i].Cookie}={Cookies[i].Value}; ";

                cookies = cookies.Substring(0, cookies.Length - 2);
                request.Headers.Add("Cookie", cookies);


                if (isPost)
                {
                    string post = string.Empty;
                    for (int i = 0, n = Params.Count; i < n; i++)
                        post = post + $"{Params[i].Param}={Params[i].Value}&";
                    post = post.Substring(0, post.Length - 1);
                    byte[] postBytes = Encoding.ASCII.GetBytes(post);
                    request.ContentLength = postBytes.Length;
                    Stream requestStream = request.GetRequestStream();


                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return html;
            }
            catch (Exception ex)
            {
                return $"Exception, {ex}";
            }
        }

        /// <summary>
        /// Make a HTTP Request
        /// </summary>
        /// <param name="URL">URL of Request</param>
        /// <param name="Host">Host of Request</param>
        /// <param name="UserAgent">UserAgent</param>
        /// <param name="ContentType">ContentType</param>
        /// <param name="Accept">Accept</param>
        /// <param name="Referer">Referer</param>
        /// <param name="proxy">Proxy</param>
        /// <param name="Params">POST Params</param>
        /// <param name="Cookies">Cookies</param>
        /// <param name="Headers">Request Headers</param>
        /// <param name="isPost">Bool for POST or GET</param>
        /// <returns>Source of Req</returns>
        public string MakeRequest(string URL,
           string Host,
           string UserAgent,
           string ContentType,
           string Accept,
           string Referer,
           WebProxy proxy,
           List<HTTPRequestParams> Params,
           List<HTTPRequestCookie> Cookies,
           List<HTTPRequestHeader> Headers,
           bool isPost
       )
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                request.Method = "POST";
                request.Host = Host.ToString();
                request.KeepAlive = true;
                request.Proxy = proxy;
                request.UserAgent = UserAgent.ToString();
                request.ContentType = ContentType.ToString();
                request.Accept = Accept.ToString();
                request.Referer = Referer.ToString();
                string cookies = string.Empty;
                for (int i = 0, n = Headers.Count; i < n; i++)
                    request.Headers.Add(Headers[i].Header, Headers[i].Value);

                for (int i = 0, n = Cookies.Count; i < n; i++)
                    cookies = cookies + $"{Cookies[i].Cookie}={Cookies[i].Value}; ";

                cookies = cookies.Substring(0, cookies.Length - 2);
                request.Headers.Add("Cookie", cookies);


                if (isPost)
                {
                    string post = string.Empty;
                    for (int i = 0, n = Params.Count; i < n; i++)
                        post = post + $"{Params[i].Param}={Params[i].Value}&";
                    post = post.Substring(0, post.Length - 1);
                    byte[] postBytes = Encoding.ASCII.GetBytes(post);
                    request.ContentLength = postBytes.Length;
                    Stream requestStream = request.GetRequestStream();


                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                string html = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return html;
            }
            catch (Exception ex)
            {
                return $"Exception, {ex}";
            }
        }
    }
}
