using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace A.DAL
{
    public static class SplitHelper
    {

        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <typeparam name="T">The type of the list.</typeparam>
        /// <param name="soruce">The list to split.</param>
        /// <param name="nSize">The size of the smaller list.</param>
        /// <returns>An list containing smaller lista.</returns>
        public static List<List<T>> Chunks<T>(this List<T> soruce, int nSize = 100)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < soruce.Count; i += nSize)
            {
                list.Add(soruce.GetRange(i, Math.Min(nSize, soruce.Count - i)));
            }

            return list;
        }
    }
}
