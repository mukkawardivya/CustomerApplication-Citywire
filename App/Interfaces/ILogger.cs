using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App
{
    public interface ILogger
    {
        void LogError(Exception ex);
    }
}
