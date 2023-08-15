using Google.Protobuf.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    internal class RankingBoard
    {
        List<Record> records = new List<Record>();

        public List<Record> GetTop5Rank()
        {
            records.Sort((a, b) =>
            {
                return a.Relic.CompareTo(b.Relic);
            });

            return records.GetRange(0, 5);
        }

        public void WriteRecord(string name, int relic)
        {
            Record record = new Record { Name = name, Relic = relic };
            records.Add(record);
        }
    }
}
