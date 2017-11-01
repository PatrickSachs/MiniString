using System.Text;

internal static class TestHelper
{
    public static string GetBitString(this byte[] bytes, char nibbleSep = '_', char byteSep = ' ')
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < bytes.Length; i++) {
            byte b = bytes[i];
            if (i != 0) {
                builder.Append(byteSep);
            }
            builder.Append(b.GetBit(7));
            builder.Append(b.GetBit(6));
            builder.Append(b.GetBit(5));
            builder.Append(b.GetBit(4));
            builder.Append(nibbleSep);
            builder.Append(b.GetBit(3));
            builder.Append(b.GetBit(2));
            builder.Append(b.GetBit(1));
            builder.Append(b.GetBit(0));
        }
        string str = builder.ToString();
        return str;
    }

    public static int GetBit(this byte b, int bitNumber)
    {
        return (b & (1 << bitNumber)) != 0 ? 1 : 0;
    }
}