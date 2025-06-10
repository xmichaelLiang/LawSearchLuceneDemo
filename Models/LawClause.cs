namespace LawSearchLuceneDemo.Models
{
    public class LawClause
    {
        public int Id { get; set; }
        public string Title { get; set; }  // ex: 刑法第10條
        public string Content { get; set; } // 條文內容
    }
}
