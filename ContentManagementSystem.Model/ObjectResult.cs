using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.Model
{
    public class ObjectResult
    {
        public int ResultCode { get; set; }

        public bool Succeeded
        {
            get
            {
                if(this.ResultCode > 0)
                 {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
