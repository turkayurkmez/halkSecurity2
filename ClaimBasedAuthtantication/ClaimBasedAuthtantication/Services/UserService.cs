using ClaimBasedAuthtantication.Models;

namespace ClaimBasedAuthtantication.Services
{
    public class UserService
    {
        private List<User> users;
        public UserService()
        {
            users = new List<User>()
            {
                new(){ Id=1, Email="abc@company.com", Name="Türkay", UserName="turkayurkmez", Password="123", Role="Admin"},
                new(){ Id=2, Email="abc@company.com", Name="Fatih", UserName="fyilmaz", Password="123", Role="Editor"},
                new(){ Id=3, Email="abc@company.com", Name="Umut", UserName="umuts", Password="123", Role="Editor"},
                new(){ Id=4, Email="abc@company.com", Name="Damla", UserName="damla", Password="123", Role="Client"},


            };


        }

        public User? ValidateUser(string userName, string password)
        {
            return users.SingleOrDefault(u => u.UserName == userName && u.Password == password);

        }
    }
}
