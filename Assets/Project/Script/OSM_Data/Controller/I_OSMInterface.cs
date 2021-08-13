using System.Collections.Generic;
using System.Threading.Tasks;
using static TreeGenerateInNodeWayRelation.I_OSMTree;


public interface I_OSMInterface
{

    Task<TreeGrp> ReadOSMNodeWayRelationAsync(string fileLink);
    void GetMapLngLat(double tlx, double tly, double brx, double bry, float radiusSquared);
}
