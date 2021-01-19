using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App;

namespace CustomerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomerRepository customerRepository = new CustomerRepository();
            customerRepository.AddCustomer("Divya", "Kotagiri", "test@test.com", new DateTime(1990,10,17), 1);
        }
    }
}
