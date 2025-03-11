namespace ProvaPub.Models
{
	public class PaginateItem<T>
	{
		public List<T> Item { get; set; }
		public int TotalCount { get; set; }
		public bool HasNext { get; set; }
	}
}
