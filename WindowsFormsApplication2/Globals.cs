using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCatalogue
{
    public static class Globals
    {
        /// <summary>
        /// Global variable that is constant.
        /// </summary>
        ///public const string GlobalString = "Important Text";

        /// <summary>
        /// Static value protected by access routine.
        /// </summary>
        static bool _adminMode;

        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static bool AdminMode
        {
            get
            {
                return _adminMode;
            }
            set
            {
                _adminMode = value;
            }
        }

        /// <summary>
        /// Global static field.
        /// </summary>
        ///public static bool GlobalBoolean;
    }
}