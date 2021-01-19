using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger _logger;

        public CustomerService(ICustomerRepository customerRepository, ILogger logger)
        {
            _customerRepository = customerRepository;
            _logger = logger;

        }
        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            try
            {
                var customer = _customerRepository.AddCustomer(firname, surname, email, dateOfBirth, companyId);
                if (customer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex);
                return false;
            }

        }
    }
}
