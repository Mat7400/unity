using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airtable
{
    public class AirTableSaver
    {
        private string idBase = "appIqXpzgnpinPJnv";
        /// <summary>
        /// how to store api key out of github?
        /// v1 not store in code or not publish on github.
        /// v2 encrypt key and store encrypted value. (aes) where store encryption pass?
        /// </summary>
        private string apikey = "fadsfasdfasdf35423423";
        public void saveToCloud(Player pl, Player enemy, int wins, int loses)
        {
            apikey = credentials.airtablekey;
            //http api to save to airtable
        }
    }
}
