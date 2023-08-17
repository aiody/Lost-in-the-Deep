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

        public List<Record> GetTop10Rank()
        {
            records.Sort((a, b) =>
            {
                return b.Relic.CompareTo(a.Relic);
            });

            return records.GetRange(0, Math.Min(records.Count, 10));
        }

        public void WriteRecord(string name, int relic)
        {
            Record record = new Record { Name = name, Relic = relic };
            records.Add(record);
        }
    }
}
