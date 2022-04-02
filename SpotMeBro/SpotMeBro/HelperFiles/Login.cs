using SQLite;

namespace SpotMeBro.HelperFiles
{
    public class LoginModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string username { get; set; }
        public string usersStoredHash { get; set; }
    }
}
