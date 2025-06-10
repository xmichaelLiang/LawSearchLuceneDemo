using LawSearchLuceneDemo.Models;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace LawSearchLuceneDemo.Services
{
    public class LuceneIndexer
    {
        private static readonly string IndexPath = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "index");

        public void CreateIndex(List<LawClause> clauses)
        {
            using var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using var directory = FSDirectory.Open(IndexPath);
            var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED);

            foreach (var clause in clauses)
            {
                var doc = new Document();
                doc.Add(new Field("Id", clause.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
                doc.Add(new Field("Title", clause.Title, Field.Store.YES, Field.Index.ANALYZED));
                doc.Add(new Field("Content", JiebaHelper.Cut(clause.Content), Field.Store.YES, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            }

            writer.Optimize();
            writer.Dispose();
        }

        public List<LawClause> Search(string keyword)
        {
            using var directory = FSDirectory.Open(IndexPath);
            using var analyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using var reader = IndexReader.Open(directory, true);
            var searcher = new IndexSearcher(reader);

            var parser = new MultiFieldQueryParser(
                Lucene.Net.Util.Version.LUCENE_30,
                new[] { "Title", "Content" },
                analyzer
            );
            var query = parser.Parse(JiebaHelper.Cut(keyword));

            var hits = searcher.Search(query, 20).ScoreDocs;

            var results = new List<LawClause>();
            foreach (var hit in hits)
            {
                var doc = searcher.Doc(hit.Doc);
                results.Add(new LawClause
                {
                    Id = int.Parse(doc.Get("Id")),
                    Title = doc.Get("Title"),
                    Content = doc.Get("Content")
                });
            }

            return results;
        }
    }
}
