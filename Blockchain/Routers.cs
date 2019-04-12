using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    static class Routers
    {
        private static Dictionary<string, Action> _getMethodList = new Dictionary<string, Action>();
        private static Dictionary<string, Action> _postMethodList = new Dictionary<string, Action>();

        public static void InitRouters()
        {
            var list = typeof(Routers).GetMethods(BindingFlags.NonPublic | BindingFlags.Static)
                .Where(x => x.GetCustomAttributes(typeof(Route), false).Length > 0)
                .Select(x =>
                    {
                        Action method = () => x.Invoke(null, null);
                        var routeInfo = x.GetCustomAttributes<Route>().First();

                        return (Method: method, RouteInfo: routeInfo);
                    })
                .ToList();
            
            list.ForEach(x =>
            {
                switch (x.RouteInfo.Method)
                {
                    case RouteMethod.GET:
                        _getMethodList.Add(x.RouteInfo.Path, x.Method);
                        break;
                    case RouteMethod.POST:
                        _postMethodList.Add(x.RouteInfo.Path, x.Method);
                        break;
                }
            });
        }

        public static void ExecuteRequest(RouteMethod method, string path)
        {
            switch (method)
            {
                case RouteMethod.GET:
                    _getMethodList[path]();
                    break;
                case RouteMethod.POST:
                    _postMethodList[path]();
                    break;
            }
        }

        [Route("/", RouteMethod.GET)]
        private static void test()
        {
            Console.WriteLine("'/' 라우터가 실행되었습니다.");
        }

        [Route("/route1", RouteMethod.GET)]
        private static void test2()
        {
            Console.WriteLine("'/route1' 라우터가 실행되었습니다.");
        }
    }
}
