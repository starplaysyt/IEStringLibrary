using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace IEStringLibrary;

public class IEString
{
    private List<char> vault = new();
    public long Length => vault.Count;
    
    public IEString(char[] array) => FromCharArray(array);
    public IEString(string str) => FromString(str);
    public IEString([NotNull] List<char> list) => FromCharList(list);
    public IEString(StringBuilder stringBuilder) => FromStringBuilder(stringBuilder);
    public IEString(char ch) => FromChar(ch);
    public IEString(byte[] array) => FromByteArray(array);

    #region FromScratch
    public void FromCharList([NotNull]List<char> list)
    {
        vault = new List<char>(list);
    }

    public void FromChar([NotNull] char ch)
    {
        vault.Clear();
        vault.Add(ch);
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
    #endregion

    #region ToScratch
    public override string ToString()
    {
        return new string(vault.ToArray());
    }
    public char[] ToCharArray()
    {
        return vault.ToArray();
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
    #endregion
    
    #region Operators
    
    public static IEString operator +([NotNull]IEString obj1, [NotNull]IEString obj2)
    {
        IEString retstr = new IEString(obj1.ToCharArray());
        for (int i = 0; i < obj2.vault.Count; i++)
        {
            retstr.vault.Add(obj2.vault[i]);
        }
        return retstr;
    }
    
    public static IEString operator +([NotNull]IEString obj1, [NotNull]string obj2)
    {
        IEString retstr = new IEString(obj1.ToCharArray());
        for (int i = 0; i < obj2.Length; i++)
        {
            retstr.vault.Add(obj2[i]);
        }

        return retstr;
    }
    
    public static IEString operator +([NotNull]string obj2, [NotNull]IEString obj1)
    {
        IEString retstr = new IEString(obj2.ToArray());
        retstr += obj1;
        return retstr;
    }
    
    public static IEString operator +([NotNull]IEString obj1, [NotNull]char obj2)
    {
        IEString retstr = new IEString(obj1.ToCharArray());
        retstr.vault.Add(obj2);
        return retstr;
    }

    public static IEString operator +([NotNull] char obj1, IEString obj2)
    {
        IEString retstr = new IEString(obj2.vault.ToArray());
        retstr.vault.Insert(0, obj1);
        return retstr;
    }
    #endregion

    public void Append(params IEString[] args)
    {
        for (int i = 0; i < args.Length; i++)
        {
            for (int j = 0; j < args[i].Length; j++)
            {
                vault.Add(args[i].vault[j]);
            }
        }
    }

    public char ElementAt(int index)
    {
        return vault[index];
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
                if (i != 0) list.Add(new IEString(timelist.ToArray()));
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
}