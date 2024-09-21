namespace LR2.Models
{
    public class Company
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Specialization { get; set; }
        public uint EmployeeCount { get; set; }

        public Company(string name, string address, string specialization, uint employeeCount)
        {
            Name = name;
            Address = address;
            Specialization = specialization;
            EmployeeCount = employeeCount;
        }

        public override string ToString()
        {
            return $"\nName: {Name}\nAddress: {Address}\nSpecialization: {Specialization}\nEmployees: {EmployeeCount}";
        }
    }
}
