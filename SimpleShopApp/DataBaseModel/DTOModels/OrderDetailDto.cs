namespace DataBaseModel.DTOModels
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string CustomerCompanyName { get; set; }
        public string SellerLastName { get; set; }
        public string SellerFirstName { get; set; }

    }
}
