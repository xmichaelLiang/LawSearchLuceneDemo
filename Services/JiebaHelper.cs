using JiebaNet.Segmenter;

namespace LawSearchLuceneDemo.Services
{
    public class JiebaHelper
    {
        private static readonly JiebaSegmenter segmenter = new JiebaSegmenter();

        public static string Cut(string text)
        {
            var words = segmenter.CutForSearch(text);
            return string.Join(" ", words);  // 用空格連接斷詞
        }
    }
}
