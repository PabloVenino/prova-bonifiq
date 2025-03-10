using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
        TestDbContext _ctx;
		public RandomService(TestDbContext ctx)
        {
            _ctx = ctx;
            seed = Guid.NewGuid().GetHashCode();
        }
        public async Task<int> GetRandom()
		{
            var number =  new Random(seed).Next(100);
            await _ctx.Numbers.AddAsync(new RandomNumber() { Number = number });
            await _ctx.SaveChangesAsync();
			return number;
		}
	}
}
