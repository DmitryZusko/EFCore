namespace DataBaseModel.DTOModels
{
    public class OrderDetailDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Amount { get; set; }
        public string SellerFullName { get; set; }
        public int SellerId { get; set; }
        public string CustomerCompany { get; set; }
        public int CustomerId { get; set; }

    }
}
