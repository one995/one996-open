using System.Collections.Generic;
using System.Threading.Tasks;

namespace Like.Common.JWT
{
    public  interface IRoleModulePermissionServices : IBaseServices<RoleModulePermission>
    {
        Task<List<RoleModulePermission>> RoleModuleMaps();
    }
}