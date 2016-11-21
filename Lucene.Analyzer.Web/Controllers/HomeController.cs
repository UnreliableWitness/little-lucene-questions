using System.IO;
using System.Text;
using System.Web.Mvc;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Nl;
using Lucene.Net.Analysis.Standard;
using Version = Lucene.Net.Util.Version;

namespace Lucene.Analyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index(string inputText)
        {
            if (string.IsNullOrEmpty(inputText) || inputText.Length > 1000)
                return View(new Analysis[] {});

            ViewBag.inputText = inputText;

            var whiteSpace = new Analysis(inputText,new WhitespaceAnalyzer(), "Whitespace Analyzer");
            var standard = new Analysis(inputText, new StandardAnalyzer(Version.LUCENE_30), "Standard Analyzer");
            var dutch = new Analysis(inputText, new DutchAnalyzer(Version.LUCENE_30), "Dutch Analyzer");
            var keyword = new Analysis(inputText, new KeywordAnalyzer(), "Keyword Analyzer");
            
            var list = new[] {whiteSpace, standard, dutch, keyword};

            return View(list);
        }


    }

    public class Analysis
    {
        public string Input { get; set; }
        public string AnalyzerTitle { get; set; }
        public int TokenCount { get; set; }

        public string Output { get; set; }

        public Analysis(string input, Net.Analysis.Analyzer analyzer, string analyzerTitle)
        {
            Input = input;
            AnalyzerTitle = analyzerTitle;
            Output = Analyze(input, analyzer);
        }

        private string Analyze(string inputText, Net.Analysis.Analyzer analyzer)
        {
            var tokenStream = analyzer.TokenStream("input", new StringReader(inputText));
            var termAttr = tokenStream.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();
            var offset = tokenStream.GetAttribute<Net.Analysis.Tokenattributes.IOffsetAttribute>();

            var sb = new StringBuilder();
            while (tokenStream.IncrementToken())
            {
                string term = termAttr.Term;
                sb.Append($"[{term}] ");
                TokenCount++;
            }

            return sb.ToString().TrimEnd(' ');
        }
    }
}