using loan_and_sell_be.Models;

namespace loan_and_sell_be.Services
{
    public interface IBeerService
    {
        public Task<List<Beer>> Get();
    }
}
