using System.Collections.Generic;
using PvPAnnouncer.Data;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class StringRepository: IStringRepository
{
    private readonly HashSet<string> _casters = [];
    public HashSet<string> GetAttributeList()
    {
        return _casters;
    }

    public void RegisterAttribute(string attr)
    {
        _casters.Add(attr);
    }

    public void DeRegisterAttribute(string attr)
    {
        _casters.Remove(attr);
    }
}