using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuecasAppUsers.Vista
{
    public class PerfilUserFlyoutMenuItem
    {
        public PerfilUserFlyoutMenuItem()
        {
            TargetType = typeof(PerfilUserFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}