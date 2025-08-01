using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * @@@ 잉크젯 통신 프로토콜 상수 값 정의
 * ESC  : 0x1B
 * CR   : 0x0D
 * LF   : 0x0A
 */
namespace Trial
{
    public static class ProtocolConstants
    {
        public const byte ESC = 0x1B;   // STARTER
        public const byte CR = 0x0D;   // CARRIAGE RETURN
        public const byte LF = 0x0A;   // LINE FEED

        public const string OK_PREFIX = "OK : ";    // OK RESPONSE DEFINITION
        public const string ER_PREFIX = "ER : ";    // NG RESPONSE DEFINITION

        public static readonly Encoding Encoding = Encoding.ASCII;

    }
}
