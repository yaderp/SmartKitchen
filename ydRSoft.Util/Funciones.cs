using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ydRSoft.Util
{
    public class Funciones
    {

        public static string LetraMayuscula(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            return char.ToUpper(input[0]) + input.Substring(1);
        }

       
        public static double LevenshteinDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; i++)
                d[i, 0] = i;
            for (int j = 0; j <= m; j++)
                d[0, j] = j;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            var dista = d[n, m];
            double respuesta = 0;
            int maxLength = Math.Max(s.Length, t.Length);
            if (maxLength > 0)
            {
                respuesta = Math.Round((1.0 - (double)dista / maxLength) * 100, 2);
            }

            return respuesta;
        }


        public static double JaccardSimilarityPercentage(string s, string t)
        {
            var set1 = new HashSet<string>(Tokenize(s));
            var set2 = new HashSet<string>(Tokenize(t));

            var intersection = new HashSet<string>(set1);
            intersection.IntersectWith(set2);

            var union = new HashSet<string>(set1);
            union.UnionWith(set2);

            // Jaccard similarity
            return Math.Round((double)intersection.Count / union.Count,2);
        }

        static IEnumerable<string> Tokenize(string text)
        {
            return text.ToLower()
                       .Split(new[] { ' ', ',', '.', ';', '!' }, StringSplitOptions.RemoveEmptyEntries)
                       .Distinct();
        }

    }
}
