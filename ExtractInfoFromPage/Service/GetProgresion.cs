using ExtractInfoFromPage.Interface;
using Interfaces;

namespace ExtractInfoFromPage.Service
{
    public class GetProgresion : IGetProgresion
    {
        public Dictionary<int, int> GetInfoProgresion(int begin, int end)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
        
            for (int i = 100; i < end; i *= 10)
            {
                var result = GetRange(i, i * 10);
                foreach (var item in result)
                {
                    dict.Add(item.Key, item.Value);
                    if (item.Value > end)
                        return dict;
                }
            }
            for (int i = 1; i < 100; i++)
            {
                dict.Add(i, i + 1);
            }
            return dict;
        }
        private Dictionary<int, int> GetRange(int beginIndex, int maxNumber)
        {
            var dict = new Dictionary<int, int>();
            int begin = beginIndex;
            while (beginIndex < maxNumber)
            {
                int index = beginIndex + begin;
                dict.Add(beginIndex, index);
                beginIndex = index;
            }
            return dict;
        }
    }
}
