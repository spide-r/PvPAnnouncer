using System.Collections.Generic;
using PvPAnnouncer.Interfaces;

namespace PvPAnnouncer.Impl;

public class AttributeRepository: IStringRepository
{
    private readonly HashSet<string> _attributes = [];
    public HashSet<string> GetAttributeList()
    {
        return _attributes;
    }

    public void RegisterAttribute(string attr)
    {
        _attributes.Add(attr);
    }

    public void DeRegisterAttribute(string attr)
    {
        _attributes.Remove(attr);
    }
}