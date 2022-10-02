using Shrooms.Contracts.Constants;
using Shrooms.Contracts.Enums;
using System;

namespace Shrooms.Domain.Extensions
{
    public static class SortDirectionExtensions
    {
        public static string GetString(this SortDirection sortDirection)
        {
            switch (sortDirection)
            {
                case SortDirection.Ascending:
                    return SortDirectionConstants.Ascending;
                case SortDirection.Descending:
                    return SortDirectionConstants.Descending;
                default:
                    throw new ArgumentException($"Sort direction {sortDirection} is invalid");
            }
        }
    }
}
