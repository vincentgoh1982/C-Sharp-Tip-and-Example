using UnityEngine;

namespace TreeGenerateInNodeWayRelation
{ 
    public class OSMAddInterface : MonoBehaviour
    {
        private Controller_OSMRequestArea osmTreeRequest;
    // Start is called before the first frame update
        void Start()
        {
            osmTreeRequest = GetComponent<Controller_OSMRequestArea>();
            osmTreeRequest.RegisterITreeChannel(new I_OSMTree()); //Generate tree using OSM data
            osmTreeRequest.RegisterITreeChannel(new I_ColorTileTree()); //Generate tree using color of the map tiles
        }

    }
}
