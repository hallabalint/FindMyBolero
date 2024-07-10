using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace FindMyBolero
{
    class HttpServer
    {

        public static HttpListener listener;
        public static string url = "http://127.0.0.2:8000/";
        public static int requestCount = 0;
        public static int pageViews = 0;
        public static string boleroIP;

        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            // While a user hasn't visited the `shutdown` url, keep on handling requests
            while (runServer)
            {
                // Will wait here until we hear from a connection
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Peel out the requests and response objects
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Print out some info about the request
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                // If `shutdown` url requested w/ POST, then shutdown the server after serving the pag

                // Make sure we don't increment the page views counter if `favicon.ico` is requested
              

                // Write the response info
                string disableSubmit = !runServer ? "disabled" : "";
                string pageData = "<!DoctypeHTML>" +
                    "<html>" +
                    "<head>" +
                    "<meta http-equiv=\"refresh\" content=\"0; url=http://"+boleroIP+"\" />" +
                    "</head>" +
                    "</html>";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                resp.RedirectLocation = "http://"+boleroIP+"/";

                // Write out to the response stream (asynchronously), then close it
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }


        public static void startServer(string IP)
        {
            // Create a Http server and start listening for incoming connections
            boleroIP = IP;
            listener = new HttpListener();
            listener.Prefixes.Add(url);
            listener.Start();
            Console.WriteLine("Listening for connections on {0}", url);

            // Handle requests
            Task listenTask = HandleIncomingConnections();
            listenTask.GetAwaiter().GetResult();

            // Close the listener
            listener.Close();
        }
    }
}
