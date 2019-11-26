#region Using Directives


#endregion

namespace Core.Entities
{
    /// <summary>
    ///     A base model that all our models should inherit from in order to have the following properties, which are set in the
    ///     ApplicationDbContext.SaveChanges method
    /// </summary>
    public abstract class BaseEntity
	{
		public int Id { get; set; }
        public string Name { get; set; }
    }
}