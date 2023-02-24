using TodoApi.Models;
using TodoApi.Repositories.Base;

namespace TodoApi.Repositories
{
    public class TradeRepository : BaseRepository<Trade>, ITradeRepository
    {
        public TradeRepository(TodoContext todoContext) : base(todoContext)
        {

        }
    }
}
