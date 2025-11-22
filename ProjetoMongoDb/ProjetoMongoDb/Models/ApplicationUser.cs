using MongoDbGenericRepository.Attributes;
using AspNetCore.Identity.MongoDbCore.Models;
namespace ProtocoloRural.Models
{
    [CollectionName("Users")]
    public class ApplicationUser : MongoIdentityUser
    {
        public string NomeCompleto { get; set; }
        public string Celular { get; set; }
    }
}
