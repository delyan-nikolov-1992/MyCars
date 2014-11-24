namespace MyCars.Models.SQLiteModels
{
    using SQLite;

    [Table("Cars")]
    public class CarSQLiteModel
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ParseObjectId { get; set; }

        public string Vendor { get; set; }

        public string Model { get; set; }

        public string Description { get; set; }

        public string OwnerUsername { get; set; }

        public string OwnerPhone { get; set; }

        public int YearOfManufacture { get; set; }

        public string CityLocation { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }
    }
}