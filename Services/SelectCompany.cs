using LR2.Models;
using static LR2.Services.SelectCompany;

namespace LR2.Services
{
    public class SelectCompany : ISelectCompany
    {
        private IConfiguration _configuration;
        public SelectCompany(IConfiguration config)
        {
            _configuration = config;
        }

        public Company GetBiggestCompany()
        {
            List<Company> companies = new List<Company>();

            foreach (var pair in _configuration.AsEnumerable())
            {
                // Перевірка ключів, що містять employeeCount
                if (pair.Key.Contains("employeeCount"))
                {
                    // Отримання секції для компанії
                    string companyKey = pair.Key.Split(':')[0]; // Використання секції для унікальних ідентифікаторів

                    string companyName = _configuration[$"{companyKey}:name"];
                    string companyAddress = _configuration[$"{companyKey}:address"];
                    string companySpecialization = _configuration[$"{companyKey}:specialization"];

                    // Перетворення к-ті працівників
                    if (uint.TryParse(pair.Value, out uint companyEmployees))
                    {
                        companies.Add(new Company(companyName, companyAddress, companySpecialization, companyEmployees));
                    }
                    else
                    {
                        Console.WriteLine($"Failed to convert employee count for {companyName}. Value: {pair.Value}");
                    }
                }
            }

            if (companies.Count > 0)
            {
                return companies.MaxBy(company => company.EmployeeCount);
            }

            Console.WriteLine("No companies were added to the list.");
            return null;
        }
    }
}
