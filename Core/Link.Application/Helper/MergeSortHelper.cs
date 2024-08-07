using Link.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Link.Application.Helper
{
    public static class MergeSortHelper
    {
        public static List<AppUser> MergeSort(List<AppUser> list, Func<AppUser, string> keySelector)
        {
            if (list.Count <= 1)
                return list;

            var middle = list.Count / 2;
            var left = list.Take(middle).ToList();
            var right = list.Skip(middle).ToList();

            return Merge(MergeSort(left, keySelector), MergeSort(right, keySelector), keySelector);
        }

        private static List<AppUser> Merge(List<AppUser> left, List<AppUser> right, Func<AppUser, string> keySelector)
        {
            var result = new List<AppUser>();
            int leftIndex = 0, rightIndex = 0;

            while (leftIndex < left.Count && rightIndex < right.Count)
            {
                if (string.Compare(keySelector(left[leftIndex]), keySelector(right[rightIndex]), StringComparison.OrdinalIgnoreCase) < 0)
                {
                    result.Add(left[leftIndex]);
                    leftIndex++;
                }
                else
                {
                    result.Add(right[rightIndex]);
                    rightIndex++;
                }
            }

            result.AddRange(left.Skip(leftIndex));
            result.AddRange(right.Skip(rightIndex));

            return result;
        }
    }

}
