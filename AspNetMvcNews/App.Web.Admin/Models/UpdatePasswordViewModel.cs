using App.Data.Entity;

namespace App.Web.Admin.Models
{
    public class UpdatePasswordViewModel
    {
        public User User { get; set; }
        public string Password { get; set; }
    }
}
