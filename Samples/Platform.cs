using System;
using System.Collections.Generic;
using System.Text;

namespace Samples
{
    public class Platform
    {

        public string OS => Environment.OSVersion.Platform.ToString();

    }
}
