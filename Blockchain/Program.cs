using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    class Program
    {
        static void array_to_sha256_computed_string()
        {
            // Python의 Dictionary와 같은 역할을 할 익명 객체
            var block = new { a = 10, b = 30 };

            // 객체를 json 문자열의 형태로 만듬 => (string) "{"a":10,"b":30}"
            var json = JsonConvert.SerializeObject(block);
            Console.WriteLine(json);

            // json 문자열을 Byte 배열로 변환
            var encoded = Encoding.ASCII.GetBytes(json);
            Console.WriteLine(string.Join(", ", encoded));

            // 이를 SHA256 암호화
            var sha = SHA256.Create().ComputeHash(encoded);
            Console.WriteLine(string.Join(", ", sha));

            // 이를 문자열화
            var shastring = BitConverter.ToString(sha).Replace("-", "").ToLower();
            Console.WriteLine(shastring);
        }


        static void Main(string[] args)
        {
            Routers.InitRouters();
            Routers.ExecuteRequest(RouteMethod.GET, "/");
        }
    }
}
