using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ILogger _logger = new AppLogger();
        public bool AddCustomer(string firname, string surname, string email, DateTime dateOfBirth, int companyId)
        {
            try
            {
                var validation = new CustomerAppValidation();
                if (validation.CustomerValidation(firname, surname, email, dateOfBirth, companyId))
                {
                    var companyRepository = new CompanyRepository();
                    var company = companyRepository.GetById(companyId);

                    var customer = new Customer
                    {
                        Company = company,
                        DateOfBirth = dateOfBirth,
                        EmailAddress = email,
                        Firstname = firname,
                        Surname = surname
                    };

                    switch (company.Name)
                    {
                        case "VeryImportantClient":
                            customer.HasCreditLimit = false;
                            break;
                        case "ImportantClient":
                            // Do credit check and double credit limit
                            customer.HasCreditLimit = true;
                            using (var customerCreditService = new CustomerCreditServiceClient())
                            {
                                var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                                creditLimit = creditLimit * 2;
                                customer.CreditLimit = creditLimit;
                            }
                            break;
                        default:
                            // Do credit check
                            customer.HasCreditLimit = true;
                            using (var customerCreditService = new CustomerCreditServiceClient())
                            {
                                var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                                customer.CreditLimit = creditLimit;
                            }
                            break;
                    }

                    //if (company.Name == "VeryImportantClient")
                    //{
                    //    // Skip credit check
                    //    customer.HasCreditLimit = false;
                    //}
                    //else if (company.Name == "ImportantClient")
                    //{
                    //    // Do credit check and double credit limit
                    //    customer.HasCreditLimit = true;
                    //    using (var customerCreditService = new CustomerCreditServiceClient())
                    //    {
                    //        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    //        creditLimit = creditLimit * 2;
                    //        customer.CreditLimit = creditLimit;
                    //    }
                    //}
                    //else
                    //{
                    //    // Do credit check
                    //    customer.HasCreditLimit = true;
                    //    using (var customerCreditService = new CustomerCreditServiceClient())
                    //    {
                    //        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                    //        customer.CreditLimit = creditLimit;
                    //    }
                    //}

                    if (customer.HasCreditLimit && customer.CreditLimit < 500)
                    {
                        return false;
                    }
                    CustomerDataAccess.AddCustomer(customer);
                    return true;
                }
                else
                {
                    Console.WriteLine("Validation unsuccessful");
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
