using CreateCustomer.API.Contracts;
using CreateCustomer.API.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;

namespace CreateCustomer.API.Repositories
{
    public class CustHoldRepository : GenericRepository<CustHold> , ICustHoldRepository
    {
        public CustHoldRepository() : base(new CustomerContext())
        {

        }

        public CustHoldRepository(CustomerContext context) : base(context)
        {

        }

        public List<CustHold> GetLateCustomersByCollector(string collector)
        {
            return _context.CustHolds
                .Include("HoldStatus")
                .Where(c => (c.Balance90 > 0 || c.Balance60 > 0 || c.Balance45 > 0) && c.Customer.Status == 1 && c.Collector == collector)
                .OrderByDescending(c => c.Balance90)
                .ThenByDescending(c => c.Balance60)
                .ThenByDescending(c => c.Balance45)
                .ToList();
        }

        public List<CustHold> GetOverCustomersByCollector(string collector)
        {
            var nonLateCustomers = _context.CustHolds
                .Include("HoldStatus")
                .Where(c => (c.TotalLate == 0 && c.Balance90 == 0 && c.Balance60 == 0 && c.Balance45 == 0) && c.Customer.Status == 1 && c.Collector == collector)
                .ToList();

            return nonLateCustomers
                .Where(c => c.Over > 0)
                .OrderByDescending(c => c.Over)
                .ToList();
        }

        public List<CustHold> GetCreditCustomersByCollector(string collector)
        {
            return _context.CustHolds
                .Include("HoldStatus")
                .Where(c => (c.Balance90 < 0 || c.Balance60 < 0 || c.Balance45 < 0) && c.Customer.Status == 1 && c.Collector == collector)
                .OrderBy(c => c.Balance90)
                .ToList();
        }

        public List<CustHold> GetCustomersOver45Days()
        {
            var custHolds = _context.CustHolds
                       .Include(c => c.HoldStatus)
                       .Include(c => c.Customer)
                       .Include(c => c.Customer.DefaultBillToAddress)
                       .Where(c => c.Balance90 <= 0 &&
                                    c.Balance60 <= 0 &&
                                    c.Balance45 > 10 &&
                                    c.TotalLate > 0 &&
                                    c.Letter45 == 0 &&
                                    c.Customer.Status == 1 &&
                                    c.HoldStatus.HoldStatusID != "VIP")
                       .ToList();

            var users = _context.Users.ToList();
            foreach (var custHold in custHolds)
            {
                custHold.User = users.First(c => c.UserID == custHold.Collector.TrimEnd());
            }

            return custHolds
                .OrderBy(c => c.User.LastName)
                .ThenBy(c => c.CustID)
                .ToList();
        }


        public List<CustHold> GetCustomersOver60Days()
        {
            var custHolds = _context.CustHolds
                            .Include(c => c.HoldStatus)
                            .Include(c => c.Customer)
                            .Include(c => c.Customer.DefaultBillToAddress)
                            .Where(c => c.Balance90 <= 0 &&
                                        c.Balance60 > 10 &&
                                        c.TotalLate > 0 &&
                                        c.Letter60 == 0 &&
                                        c.Customer.Status == 1 &&
                                        c.HoldStatus.HoldStatusID != "VIP")
                            .ToList();

            var users = _context.Users.ToList();
            foreach (var custHold in custHolds)
            {
                custHold.User = users.First(c => c.UserID == custHold.Collector.TrimEnd());
            }

            return custHolds
                .OrderBy(c => c.User.LastName)
                .ThenBy(c => c.CustID)
                .ToList();
        }

        public List<CustHold> GetCustomersOver90Days()
        {
            var custHolds =  _context.CustHolds
                                    .Include(c => c.HoldStatus)
                                    .Include(c => c.Customer)
                                    .Include(c => c.Customer.DefaultBillToAddress)
                                    .Where(c => c.Balance90 > 10 &&
                                                 c.TotalLate > 0 &&
                                                 c.Letter90 == 0 &&
                                                 c.Customer.Status == 1 &&
                                                 c.HoldStatus.HoldStatusID != "VIP" &&
                                                 !c.CustID.Contains("Hussm"))
                                    .ToList();

            var users = _context.Users.ToList();
            foreach (var custHold in custHolds)
            {
                custHold.User = users.First(c => c.UserID == custHold.Collector.TrimEnd());
            }

            return custHolds
                .OrderBy(c => c.User.LastName)
                .ThenBy(c => c.CustID)
                .ToList();
        }
    }

}
