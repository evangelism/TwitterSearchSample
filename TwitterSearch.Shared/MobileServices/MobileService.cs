using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterSearch.MobileServices
{
    public class MobileService
    {

        public static MobileServiceClient Srv = new MobileServiceClient(
        "https://twisearch.azure-mobile.net/", "TVcDaKuSVTPXNPEOmyXWmgIwDLLCuy61");

        public static async void Update(string word)
        {
            var table = Srv.GetTable<Stats>();
            var res = await (from z in table where z.id == word select z).ToListAsync();
            if (res.Count > 0)
            {
                var s = res[0];
                s.count++;
                await table.UpdateAsync(s);
            }
            else
            {
                var s = new Stats() { id = word, count = 1 };
                await table.InsertAsync(s);
            }
        }

        // TODO: This is a very bad in-memory implementation
        // Need to refactor this code to run on server-side
        // Problem is that orderby and max are not supported in table queries
        public static async Task<Stats> GetPopular()
        {
            var table = Srv.GetTable<Stats>();
            var res = await (from z in table select z).ToListAsync();
            var x = (from z in res orderby z.count descending select z).FirstOrDefault();
            return x;
        }

    }
}

