using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;


namespace pachongchengxv
{
    public class Crawler
    {
        public Uri StartUrl { get; set; }
        private HashSet<Uri> visited = new HashSet<Uri>();
        private readonly string[] allowedExtensions = { ".htm", ".html", ".aspx", ".php", ".jsp" };

        public event Action<string> PageCrawled;
        public event Action<string, string> ErrorOccurred;

        public async Task Start()
        {
            var queue = new ConcurrentQueue<Uri>();
            queue.Enqueue(StartUrl);
            visited.Add(StartUrl);

            while (queue.TryDequeue(out Uri currentUri))
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await client.GetAsync(currentUri);
                        if (response.IsSuccessStatusCode)
                        {
                            var contentType = response.Content.Headers.ContentType?.MediaType;
                            if (IsAllowedContent(contentType, currentUri))
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                PageCrawled?.Invoke(currentUri.ToString());

                                var links = ParseLinks(content, currentUri);
                                foreach (var link in links)
                                {
                                    if (!visited.Contains(link))
                                    {
                                        visited.Add(link);
                                        queue.Enqueue(link);
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(currentUri.ToString(), ex.Message);
                }
            }
        }

        private bool IsAllowedContent(string contentType, Uri uri)
        {
            if (contentType?.Contains("text/html") == true) return true;
            return allowedExtensions.Contains(Path.GetExtension(uri.AbsolutePath).ToLower());
        }

        private List<Uri> ParseLinks(string html, Uri baseUri)
        {
            var links = new List<Uri>();
            var regex = new Regex(@"href\s*=\s*[""']?(.*?)[""'\s>]", RegexOptions.IgnoreCase);

            foreach (Match match in regex.Matches(html))
            {
                var value = match.Groups[1].Value;
                if (Uri.TryCreate(baseUri, value, out Uri absoluteUri))
                {
                    if (absoluteUri.Host.Equals(baseUri.Host, StringComparison.OrdinalIgnoreCase) &&
                        IsAllowedContent(null, absoluteUri))
                    {
                        links.Add(absoluteUri);
                    }
                }
            }
            return links;
        }
    }
}