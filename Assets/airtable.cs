using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

using System.IO;
using System.Net.Http;
using System.Net;

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
        private string apikey = "000";
        private string url= "https://api.airtable.com/v0/appIqXpzgnpinPJnv/unitygame";
        public string saveToCloud(Player pl, Player enemy, int wins, int loses)
        {
            apikey = "--";
            //http api to save to airtable
            //1) Собрать данные в нужные формат JSON
            //2) передать эти данные через HTTP запрос
            //   (https://api.airtable.com/v0/appIqXpzgnpinPJnv/unitygame)
            unityGameTable table = new unityGameTable();
            table.guid = pl.gid;
            table.playerName = pl.name;
            table.wins = wins;
            table.loses = loses;
            string data = "";
            table.damageByEnemy = pl.dealedDamage.Sum();
            table.dameByPlayer = enemy.dealedDamage.Sum();
            //to json string
           
//json    библиотеки сторонние совместно с Юнити не легко загрузить
//поэтому надо использовать встроенные в С# штуки
            using (FileStream fs = new FileStream("example.json", FileMode.OpenOrCreate))
            {
                //var writer = JsonReaderWriterFactory.CreateJsonWriter(sw.BaseStream);
                //writer.WriteStartElement("guid");
                //writer.WriteValue(table.guid);
                //writer.WriteEndElement();
                //writer.Flush();
                //writer.Close();
                DataContractJsonSerializer formatter = new DataContractJsonSerializer(typeof(unityGameTable));
                formatter.WriteObject(fs, table);
                MemoryStream ms = new MemoryStream();
                formatter.WriteObject(ms, table);
                //HTTP 
                var request = WebRequest.Create(url);
                request.Method = "POST";


                ////UNPROCESSABLE ENTITY
                //нужны поля records и fields как в документации
                //https://airtable.com/appIqXpzgnpinPJnv/api/docs#curl/table:unitygame:create
                var json = Encoding.UTF8.GetString(ms.ToArray());

                byte[] byteArray = Encoding.UTF8.GetBytes(json);

                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                request.Headers.Add("Authorization: Bearer " + apikey);

                var reqStream = request.GetRequestStream();
                reqStream.Write(byteArray, 0, byteArray.Length);

                var response = request.GetResponse();
                //Console.WriteLine(((HttpWebResponse)response).StatusDescription);

                var respStream = response.GetResponseStream();

                var reader = new StreamReader(respStream);
                 data = reader.ReadToEnd();
                //Console.WriteLine(data);
                
            }
            return data;
            //следующий этап - почитать документацию ЭйрТейбл и понять какой им нужен формат JSON
        }
    }
    //этот класс легко преобразовать в JSON формат
    public class unityGameTable
    {
        public string guid;
        public string playerName;
        public int dameByPlayer;

        public int damageByEnemy;
        public int wins;
        public int loses;
    }
}
