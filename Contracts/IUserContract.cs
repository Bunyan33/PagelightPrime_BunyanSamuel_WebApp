using PagelightPrime_BunyanSamuel_WebApp.Models;

namespace PagelightPrime_BunyanSamuel_WebApp.Contracts
{
    public interface IUserContract
    {
        public void CreateUser(User user);

        public IEnumerable<User> ReadAllUser();

        public User FindUserById(int Id);

        public void UpdateUser(User user);

        public void DeleteUser(int Id);

    }
}
