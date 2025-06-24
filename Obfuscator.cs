using System;
using System.Text.RegularExpressions;

public class Obfuscator{
    
    public static string? ToNumber(string ipAddress)
    {
        byte[] _bytes = [0, 0, 0, 0];

        var chunks = ipAddress.Split(".");
        if (chunks.Length != 4)
        {
            return null;
        }

        //Convert to bytes
        var i = 0;
        foreach (String chunk in chunks)
        {
            _bytes[i] = Convert.ToByte(chunk);
            i += 1;
        }

        //Convert to valid number
        var obfuscated_bytes = (_bytes[0] << 24) | (_bytes[1] << 16) | (_bytes[2] << 8) | _bytes[3];
        return obfuscated_bytes.ToString();
    } 

        
    public static string? ToOct(string ipAddress)
    {
        string[] n = ipAddress.Split('.');
        if (n.Length != 4)
        {
            return null;
        }

        string[] octParts = new string[4];

        for (int i = 0; i < 4; i++)
        {
            if (!int.TryParse(n[i], out int num))
            {
                return null;
            }

            if (num > 255 || num < 0)
            {
                return null;
            }

            int one = num / 64;
            int t = num % 64;
            int two = t / 8;
            int three = num % 8;

            octParts[i] = "0" + one + two + three;
        }

        string octip = string.Join(".", octParts);
        return octip;
    }

    public static string? ToHex(string ipAddress)
    {
        string[] n = ipAddress.Split('.');
        if (n.Length != 4)
        {
            return null;
        }

        string[] hexParts = new string[4];
        
        for (int i = 0; i < 4; i++)
        {
            if (!int.TryParse(n[i], out int num))
            {
                return null;
            }

            if (num > 255 || num < 0)
            {
                return null;
            }

            string two = NumLet(num % 16);
            string one = NumLet(num / 16);
            
            hexParts[i] = "0x" + one + two;
        }

        string hexip = string.Join(".", hexParts);
        return hexip;
    }

    private static string NumLet(int num)
    {
        if (num < 10)
        {
            return num.ToString();
        }
        return ((char)('A' + num - 10)).ToString();
    }
}
