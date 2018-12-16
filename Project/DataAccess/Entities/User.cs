using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string MainPhotoPath { get; set; }
        public string NickName { get; set; }
        public string Password { get; set; }
		public ICollection<Photo> Photos { get; set; }
		public ICollection<User> Followers { get; set; }
		public ICollection<User> Following { get; set; }
		public bool IsAdmin { get; set; }
    }
}
