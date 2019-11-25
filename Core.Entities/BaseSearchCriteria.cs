namespace Core.Entities.SearchCriteria
{
    /// <summary>
    ///     Common properties of all the search criteria. Used to get paged results.
    ///     
    ///     Search criteria classes typically contain the criteria the user used to do a search. But can be used also for all search (API searches etc)
    ///     We cannot use the core Entity/POCO, because we may need:
    ///         - Paging
    ///         - Sorting
    ///         - Unlike to core Entity/POCO all properties should be nullable
    ///         - For some fields such as dates, numeric values, amounts, we may need:
    ///             - an upper limit
    ///             - a lower limit
    ///             - both -> range
    ///           instead of exact search values
    ///         
    /// *POCO means Plain Old C# Object or Plain Old CRL Object. https://en.wikipedia.org/wiki/Plain_old_CLR_object
    /// </summary>
    public class BaseSearchCriteria
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public string ThenBy { get; set; }
    }
}
