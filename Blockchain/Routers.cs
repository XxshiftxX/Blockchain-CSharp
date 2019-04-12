using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    class Routers
    {
        public readonly int Port;
        private Dictionary<string, Action<Routers>> _getMethodList = new Dictionary<string, Action<Routers>>();
        private Dictionary<string, Action<Routers>> _postMethodList = new Dictionary<string, Action<Routers>>();

        public Routers(int port)
        {
            Port = port;
            var list = typeof(Routers).GetMethods(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => x.GetCustomAttributes(typeof(Route), false).Length > 0)
                .Select(x =>
                    {
                        Action<Routers> method = (obj) => x.Invoke(obj, null);
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

            Console.WriteLine($"{Port}번 포트에서 대기 중.");
        }

        public void ExecuteRequest(RouteMethod method, string path)
        {
            Console.WriteLine();
            switch (method)
            {
                case RouteMethod.GET:
                    _getMethodList[path](this);
                    break;
                case RouteMethod.POST:
                    _postMethodList[path](this);
                    break;
            }
        }

        [Route("/", RouteMethod.GET)]
        private void test()
        {
            Console.WriteLine("'/' 라우터가 실행되었습니다.");
        }

        [Route("/route1", RouteMethod.GET)]
        private void test2()
        {
            Console.WriteLine("'/route1' 라우터가 실행되었습니다.");
        }
    }
}
