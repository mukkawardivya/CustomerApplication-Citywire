using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public interface ICustomerRepository
    {
        bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId);
    }
}
