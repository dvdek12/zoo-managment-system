namespace ZooManagmentSystem.DTOs.Employee
{
    // DTO dla pracownika - tylko jego własne dane
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? BirthDay { get; set; }
    }

    // DTO dla managera - może zmieniać rolę
    public class UpdateEmployeeManagerDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? RoleId { get; set; }
    }
}
