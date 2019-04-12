using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    class Route : Attribute
    {
        public readonly RouteMethod Method;
        public readonly string Path;

        public Route(string path, RouteMethod method)
        {
            Path = path;
            Method = method;
        }
    }

    enum RouteMethod
    {
        GET, POST
    }
}
