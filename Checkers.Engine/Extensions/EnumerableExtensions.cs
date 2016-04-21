using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkers.Engine.Extensions
{
    public static class EnumerableExtensions
    {
        public static T Random<T>(this IEnumerable<T> set)
        {
            var rand = new Random();
            var array = set as T[] ?? set.ToArray();
            var theOne = rand.Next(array.Length);

            return array[theOne];
        }
    }
}
