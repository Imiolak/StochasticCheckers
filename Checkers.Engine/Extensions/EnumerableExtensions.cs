using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Engine.Extensions
{
    public static class EnumerableExtensions
    {
        private static readonly Random Rand = new Random();

        public static T Random<T>(this IEnumerable<T> set)
        {
            var array = set as T[] ?? set.ToArray();
            var theOne = Rand.Next(array.Length);

            return array[theOne];
        }
    }
}
