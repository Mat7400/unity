using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

using System.IO;

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
        public void saveToCloud(Player pl, Player enemy, int wins, int loses)
        {
            //apikey = credentials.airtablekey;
            //http api to save to airtable
            //1) Собрать данные в нужные формат JSON
            //2) передать эти данные через HTTP запрос
            //   (https://api.airtable.com/v0/appIqXpzgnpinPJnv/unitygame)
            unityGameTable table = new unityGameTable();
            table.guid = pl.gid;
            table.playerName = pl.name;
            table.wins = wins;
            table.loses = loses;

            table.damageByEnemy = pl.dealedDamage.Sum();
            table.dameByPlayer = enemy.dealedDamage.Sum();
            //to json string
           
//json    библиотеки сторонние совместно с Юнити не легко загрузить
//поэтому надо использовать встроенные в С# штуки
            using (StreamWriter sw = File.CreateText("example.json"))
            {
                var writer = JsonReaderWriterFactory.CreateJsonWriter(sw.BaseStream);
                writer.WriteStartElement("guid");
                writer.WriteValue(table.guid);
                writer.WriteEndElement();
                writer.Flush();
                writer.Close();
            }
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
