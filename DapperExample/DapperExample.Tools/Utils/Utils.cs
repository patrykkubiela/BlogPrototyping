using System;

namespace DapperExample.Tools
{
    public static class Utils
    {
        public static byte[] GetRandomByteArray(int arraySize)
        {
            Random rnd = new Random();
            Byte[] b = new Byte[arraySize];
            rnd.NextBytes(b);
            return b;
        }
    }
}