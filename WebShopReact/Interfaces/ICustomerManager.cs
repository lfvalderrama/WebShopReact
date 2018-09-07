using WebShopReact.Models;

namespace WebShopReact.Interfaces
{
    public interface ICustomerManager
    {
        object Authenticate(LoginInput customerIn);
        Customer GetCustomer(int id);
        bool AddCustomer(CustomerDTO customerIn);
        void UpdateCustomer(Customer customer);
    }

}
