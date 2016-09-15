namespace Eng.Vector.Framework.UnitTest
{
    using System.Text;

    internal static class Extensions
    {
        internal static byte[] ToByteArray(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
