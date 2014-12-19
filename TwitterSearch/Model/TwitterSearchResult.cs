using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch
{
    public class TwitterSearchResult : NotifyBase
    {
        private ObservableCollection<Tweet> items = new ObservableCollection<Tweet>();
        public ObservableCollection<Tweet> Items
        {
            get { return items; }
            set
            {
                if (value != items)
                {
                    items = value;
                    NotifyPropertyChanged("Items");
                }
            }
        }

        public async Task Search(string searchTerm)
        {
            //Tuple<List<ExpandoObject>, List<ExpandoObject>> _return =
            //    new Tuple<List<ExpandoObject>, List<ExpandoObject>>(new List<ExpandoObject>(), new List<ExpandoObject>());

            const string oauthConsumerKey = "kYvsjTzFLXSMmDm3yVqDHSHM1";
            const string oauthConsumerSecret = "GdZVmbRawDa4HWS9AMMDMpa8ALrE9AxkNZsaM5rLzQet2EFXbH";

            string accessToken;

            // get authentication token
            HttpMessageHandler handler = new HttpClientHandler();
            HttpClient httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding().GetBytes(oauthConsumerKey + ":" + oauthConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            var s = await response.Content.ReadAsStringAsync();
            var returnJson = JValue.Parse(s);
            accessToken = returnJson["access_token"].ToString();

            // search 
            HttpRequestMessage requestSearch = new HttpRequestMessage(HttpMethod.Get, "https://api.twitter.com/1.1/search/tweets.json?count=100&q=" + searchTerm);
            requestSearch.Headers.Add("Authorization", "Bearer " + accessToken);
            HttpResponseMessage responseSearch = await httpClient.SendAsync(requestSearch);
            var returnJsonSearch = JValue.Parse(await responseSearch.Content.ReadAsStringAsync());

            Items.Clear();

            foreach (var token in returnJsonSearch["statuses"])
            {
                var searchResult = new Tweet();
                searchResult.ID = token["id"].ToString();
                searchResult.Author = token["user"]["name"].ToString();
                searchResult.AvatarUrl = token["user"]["profile_image_url"].ToString();
                searchResult.Body = token["text"].ToString();
                searchResult.Geo = token["user"]["location"].ToString();

                this.Items.Add(searchResult);
            }

            NotifyPropertyChanged("Items");
        }

    }
}
