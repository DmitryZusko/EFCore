namespace DataBaseModel.DatabaseModels
{
    using System.ComponentModel.DataAnnotations;
    public class Customer
    {
        public int Id { get; set; }
        public string Company { get; set; }
    }
}