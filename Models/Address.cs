namespace Models
{
    public class Address
    {
        #region Properties
        public int Id { get; set; }
        public string? Street { get; set; }
        public int? Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? CEP { get; set; }
        public string? Description { get; set; }
        public City City { get; set; }
        public string? RegisterDate { get; set; }
        #endregion
    }
}