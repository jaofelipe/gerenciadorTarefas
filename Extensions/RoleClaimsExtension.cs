using GerenciadorTarefas.Models;
using System.Data;
using System.Security.Claims;

namespace GerenciadorTarefas.Extensions
{
    public static class RoleClaimsExtension
    {
        public static IEnumerable<Claim> GetClaims(this User user)
        {
            var result = new List<Claim>
            {
                new (ClaimTypes.Name, user.Email),
                new (ClaimTypes.NameIdentifier, user.Id.ToString()),

            };

            result.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Slug)));

            return result;
        }

        public static bool IsAdmin(this ClaimsPrincipal user) => user.IsInRole("admin");

        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }

        public static Guid? GetRoleId(this User user) => user.Roles?.FirstOrDefault()?.Id;

    }
}
