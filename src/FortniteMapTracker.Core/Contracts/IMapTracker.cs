using FortniteMapTracker.Core.Models;

namespace FortniteMapTracker.Core.Contracts
{
    interface IMapTracker
    {
        Coord Match(string minimap, string world);
    }
}
