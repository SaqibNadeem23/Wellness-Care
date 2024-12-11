using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Welness_Care
{
    public class UserMasterPageFlyoutMenuItem
    {
        public UserMasterPageFlyoutMenuItem()
        {
            TargetType = typeof(UserMasterPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}