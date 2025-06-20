using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExternalDataService.Configuration
{
    public class CacheSettings
    {
        public int UserCacheMinutes { get; set; } = 10;
        public int AllUserCacheMinutes { get; set; } = 10;
    }  
}
