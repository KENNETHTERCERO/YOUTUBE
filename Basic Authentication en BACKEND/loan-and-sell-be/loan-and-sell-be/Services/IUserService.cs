namespace loan_and_sell_be.Services
{
    public interface IUserService
    {
        public bool IsUser(string email, string pass);
    }
}
