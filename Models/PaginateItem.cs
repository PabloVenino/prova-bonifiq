namespace ProvaPub.Models
{
	public class PaginateItem<T> where T : class
	{
		public List<T> Item { get; set; }
		public int TotalCount { get; set; }
		public bool HasNext { get; set; }
	}
}
