using System.Collections.Generic;
using PvPAnnouncer.Data;

namespace PvPAnnouncer.Interfaces;

public interface IStringRepository
{
    public HashSet<string> GetAttributeList();
    public void RegisterAttribute(string attr);
    public void DeRegisterAttribute(string attr);
}