namespace Service.Customers
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }

        public CustomerDto(int v1, string v2, string v3)
        {
            this.Id = v1;
            this.Nombre = v2;
            this.Email = v3;
        }

    }
}
