using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Analysis;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace LucenePhraseQuery
{
    public class Program
    {
        static void Main(string[] args)
        {



            var indexSearcher = new IndexSearcher(directory);
            var query = new TermQuery(new Term("content", "kat"));
            var result = indexSearcher.Search(query, 10);

        }
    }
}
