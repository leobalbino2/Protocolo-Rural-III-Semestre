using MongoDbGenericRepository.Attributes;
using AspNetCore.Identity.MongoDbCore.Models;
namespace ProtocoloRural.Models
{
    [CollectionName("Roles")]
    public class ApplicationRole : MongoIdentityRole
    {
    }
}
