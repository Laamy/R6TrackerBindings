using System.Runtime.CompilerServices;

public class NetPacket
{
    public dynamic _t;

    public dynamic this[dynamic index]
    {
        get
        {
            try
            {
                if (_t[index] != null)
                    return _t[index];
            }
            catch { }

            return "NULLnot ";
        }
    }

    public NetPacket(dynamic t) => _t = t;
}