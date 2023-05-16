using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldstateExpressionReader
{
    public interface IDbContext
    {
        Task<string?> GetWorldstateName(Int32 Id);
        Task<string?> GetWorldstateExpression(Int32 Id);
        Task<List<WorldstateExpression>?> GetAllExpressions();
    }
}
