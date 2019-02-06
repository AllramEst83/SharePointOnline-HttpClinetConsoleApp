using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp1
{
    public class HttpExecutor
    {
        public Uri webUri { get; set; } = new Uri("https://codedbykay.sharepoint.com/sites/kaysdevsite");
        public string userName { get; set; } = "kay@Codedbykay.onmicrosoft.com";
        public string password { get; set; } = "RebeckaVirgin81";
        //VERBS
        //GetAll
        internal void Get()
        {
            using (var client = new SPHttpClient(webUri, userName, password))
            {
                var listTitle = "HttpClientList";
                var endpointUrl = string.Format("{0}/_api/web/lists/getbytitle('{1}')/items?$select=ID%2CTitle%2CSomecolumn", webUri, listTitle);
                var data = client.ExecuteJson(endpointUrl);
                foreach (var item in data["d"]["results"])
                {
                    Console.WriteLine(item["Title"] + " - " + "id: " + item["ID"]);
                }
            }
        }
        //GetOne
        internal void GetOne(int itemId)
        {
            using (var client = new SPHttpClient(webUri, userName, password))
            {
                var listTitle = "HttpClientList";
                //var itemId = 7;
                var endpointUrl = string.Format("{0}/_api/web/lists/getbytitle('{1}')/items({2})", webUri, listTitle, itemId);
                var data = client.ExecuteJson(endpointUrl);
                Console.WriteLine("Column 1: " + data["d"]["Title"] + ". Column 2: " + data["d"]["Somecolumn"] + ". - id: " + data["d"]["ID"]);
            }
        }
        //Post
        internal void Post(string title, string value){

            using (var client = new SPHttpClient(webUri, userName, password))
            {
                var listTitle = "HttpClientList";
                var itemPayload = new
                {
                    __metadata = new
                    {
                        type = "SP.Data.HttpClientListListItem"
                    },
                    //All the columns goes here comma seperetad
                    Title = title,
                    Somecolumn = value
                };
                var endpointUrl = string.Format("{0}/_api/web/lists/getbytitle('{1}')/items", webUri, listTitle);
                var data = client.ExecuteJson(endpointUrl, HttpMethod.Post, itemPayload);
                Console.WriteLine("Task item '{0}' has been created", data["d"]["Title"]);
          
            }
        }
        //Put
        internal void Put(string title, string value, int itemId)
        {

            using (var client = new SPHttpClient(webUri, userName, password))
            {
                var listTitle = "HttpClientList";
                //var itemId = 6;
                var itemPayload = new { __metadata = new { type = "SP.Data.HttpClientListListItem" },
                    //All the columns goes here comma seperetad
                    Title = title,
                    Somecolumn = value
                };
                var endpointUrl = string.Format("{0}/_api/web/lists/getbytitle('{1}')/items({2})", webUri, listTitle, itemId);
                var headers = new Dictionary<string, string>();
                headers["IF-MATCH"] = "*";
                headers["X-HTTP-Method"] = "MERGE";
                client.ExecuteJson(endpointUrl, HttpMethod.Post, headers, itemPayload);
                Console.WriteLine("Task item has been updated");
             
            }
        }
        //Delete
        internal void Delete(int itemId)
        {
            using (var client = new SPHttpClient(webUri, userName, password))
            {
                var listTitle = "HttpClientList";
                //var itemId = 2;
                var endpointUrl = string.Format("{0}/_api/web/lists/getbytitle('{1}')/items({2})", webUri, listTitle, itemId);
                var headers = new Dictionary<string, string>();
                headers["IF-MATCH"] = "*";
                headers["X-HTTP-Method"] = "DELETE";
                client.ExecuteJson(endpointUrl, HttpMethod.Post, headers, default(string));
                Console.WriteLine("Task item has been deleted");
          
            }
        }
        //VERBS
        //Methods
        //ExtractId
        internal int ExtractId(string uri)
        {

            var name = uri;
            int start = name.LastIndexOf("(");
            int end = name.LastIndexOf(")");
            string result = name.Substring(start + 1, end - start - 1);

            return Int32.Parse(result);
        }
        //ExtractId
        //isURLExist
        public static void isURLExist(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);

                WebResponse res = req.GetResponse();

                Console.WriteLine("Url Exists");
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.Message.Contains("remote name could not be resolved"))
                {
                    Console.WriteLine("Url is Invalid");
                }
            }
        }
        //isURLExist

        //Methods
    }//End
}
