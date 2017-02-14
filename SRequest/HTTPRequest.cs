using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SRequest
{
    public class HTTPRequest
    {
        HttpWebRequest request;
        Stream requestStream;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        public HTTPRequest(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
        }

        /// <summary>
        /// Set the headers
        /// </summary>
        /// <param name="Headers">List of headers</param>
        public void SetHeaders(List<HTTPRequestHeader> Headers)
        {
            for (int i = 0, n = Headers.Count; i < n; i++)
                request.Headers.Add(Headers[i].Header, Headers[i].Value);
        }

        /// <summary>
        /// Set the cookies
        /// </summary>
        /// <param name="Cookies">List of cookies</param>
        public void SetCookies(List<HTTPRequestCookie> Cookies)
        {
            string cookies = string.Empty;
            for (int i = 0, n = Cookies.Count; i < n; i++)
                cookies = cookies + $"{Cookies[i].Cookie}={Cookies[i].Value}; ";

            cookies = cookies.Substring(0, cookies.Length - 2);
            request.Headers.Add("Cookie", cookies);
        }

        /// <summary>
        /// Set the post parameters
        /// </summary>
        /// <param name="Post">List of params</param>
        public void SetPost(List<HTTPRequestParams> Post)
        {
            string post = string.Empty;
            for (int i = 0, n = Post.Count; i < n; i++)
                post = post + $"{Post[i].Param}={Post[i].Value}&";
            post = post.Substring(0, post.Length - 1);
            byte[] postBytes = Encoding.ASCII.GetBytes(post);
            request.Method = "POST";
            request.ContentLength = postBytes.Length;
            Stream requestStream = request.GetRequestStream();

            requestStream.Write(postBytes, 0, postBytes.Length);
            requestStream.Close();
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
        /// <returns>Source of Req</returns>
        public string Execute(
            string Host,
            string UserAgent,
            string ContentType,
            string Accept,
            string Referer,
            List<HTTPRequestParams> Post,
            bool IsPost
        )
        {
            try
            {
                request.Host = Host.ToString();
                request.KeepAlive = true;
                request.UserAgent = UserAgent.ToString();
                request.ContentType = ContentType.ToString();
                request.Accept = Accept.ToString();
                request.Referer = Referer.ToString();

                if(IsPost)
                {
                    SetPost(Post);
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
        /// <returns>Source of Req</returns>
        public string Execute(
           string Host,
           string UserAgent,
           string ContentType,
           string Accept,
           string Referer,
           WebProxy proxy, 
           List<HTTPRequestParams> Post,
           bool IsPost
       )
        {
            try
            {
                request.Host = Host.ToString();
                request.KeepAlive = true;
                request.Proxy = proxy;
                request.UserAgent = UserAgent.ToString();
                request.ContentType = ContentType.ToString();
                request.Accept = Accept.ToString();
                request.Referer = Referer.ToString();

                if (IsPost)
                {
                    SetPost(Post);
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
