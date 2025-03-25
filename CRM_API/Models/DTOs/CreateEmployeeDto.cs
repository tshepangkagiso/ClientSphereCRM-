namespace CRM_API.Models.DTOs
{
    public class CreateEmployeeDto
    {
        public int TitleID { get; set; }

        public string EmployeeName { get; set; } = null!;

        public string EmployeeSurname { get; set; } = null!;

        public string LoginUsername { get; set; } = null!;

        public string LoginPassword { get; set; } = null!;
    }
}
