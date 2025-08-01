using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @@@ 잉크젯 통신 서버 애뮬레이터 실행
 */
namespace EmulatorTrial
{
    class Program
    {
        static void Main()
        {
            var server = new EmulatorServer(9100); // 포트는 설비에 따라 조정 (9100: Inkjet, 9200 : AMR)
            server.Start();

            Console.WriteLine("잉크젯 에뮬레이터 서버가 시작되었습니다.");
            Console.ReadLine();
            server.Stop();
        }
    }

}
