using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IEStringLibrary;

public class IEString
{
    private List<char> vault = new();
    public IEString(char[] array)
    {
        FromCharArray(array);
    }

    public IEString(string str)
    {
        FromString(str);
    }

    public IEString([NotNull]List<char> list)
    {
        FromCharList(list);
    }

    public IEString(StringBuilder stringBuilder)
    {
        FromStringBuilder(stringBuilder);
    }

    public void FromCharList([NotNull]List<char> list)
    {
        vault = new List<char>(list);
    }

    public bool Equals(IEString obj)
    {
        if (obj.vault.Count != vault.Count) return false;
        for (int i = 0; i < vault.Count; i++)
        {
            if (obj.vault[i] != vault[i])
            {
                return false;
            }
        }

        return true;
    }

    public void FromStringBuilder(StringBuilder stringBuilder)
    {
        vault.Clear();
        for (int i = 0; i < stringBuilder.Length; i++)
        {
            vault.Add(stringBuilder[i]);
        }
    }

    public void FromString(string str)
    {
        vault = new List<char>(str.ToCharArray());
    }

    public override string ToString()
    {
        return new string(vault.ToArray());
    }

    public char ElementAt(int index)
    {
        return vault[index];
    }

    public char[] ToCharArray()
    {
        return vault.ToArray();
    }

    public static IEString operator +([NotNull]IEString obj1, [NotNull]IEString obj2)
    {
        for (int i = 0; i < obj2.vault.Count; i++)
        {
            obj1.vault.Add(obj2.vault[i]);
        }

        return new IEString(obj1.vault);
    }
    
    public static IEString operator +([NotNull]IEString obj1, [NotNull]string obj2)
    {
        for (int i = 0; i < obj2.Length; i++)
        {
            obj1.vault.Add(obj2[i]);
        }

        return new IEString(obj1.vault);
    }
    public static IEString operator +([NotNull]IEString obj1, [NotNull]char obj2)
    {
        obj1.vault.Add(obj2);
        return new IEString(obj1.vault);
    }

    public StringBuilder ToStringBuilder()
    {
        StringBuilder toret = new StringBuilder();
        for (int i = 0; i < vault.Count; i++)
        {
            toret.Append(vault[i]);
        }

        return toret;
    }

    public void FromCharArray(char[] array)
    {
        vault = new List<char>(array);
    }

    public void FromByteArray(byte[] array)
    {
        vault.Clear();
        for (int i = 0; i < array.Length; i++)
        {
            vault.Add((char)array[i]);
        }
    }

    public bool Contains(char symbol)
    {
        return vault.Contains(symbol);
    }

    public void Clear()
    {
        vault.Clear();
    }

    public IEString[] Split(char separator)
    {
        List<IEString> list = new List<IEString>();
        List<char> timelist = new List<char>();
        for (int i = 0; i < vault.Count; i++)
        {
            if (vault[i] == separator)
            {
                list.Add(new IEString(timelist.ToArray()));
                timelist.Clear();
            }
            else
            {
                timelist.Add(vault[i]);
            }
        }
        list.Add(new IEString(timelist));
        return list.ToArray();
    }
}