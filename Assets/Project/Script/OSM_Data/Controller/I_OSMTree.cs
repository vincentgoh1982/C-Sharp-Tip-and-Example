
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace TreeGenerateInNodeWayRelation
{
    public class I_OSMTree: I_OSMInterface
    {
        public class TreeGrp
        {
            public List<double> lng = new List<double>();
            public List<double> lat = new List<double>();
            public bool treeDefineByColor = false;
        }

        public TreeGrp treeGrp = new TreeGrp();

        private List<OnlineMapsOSMNode> nodes;
        private List<OnlineMapsOSMWay> ways;
        private List<OnlineMapsOSMRelation> relations;

        private Vector2 topLeftXTopLeftY;
        private Vector2 bottomRightXBottomRightY;
        private float radiusSquared;

        void I_OSMInterface.GetMapLngLat(double tlx, double tly, double brx, double bry, float radius)
        {
            topLeftXTopLeftY = new Vector2((float)tlx, (float)tly);
            bottomRightXBottomRightY = new Vector2((float)brx, (float)bry);
            radiusSquared = radius;
        }

        async Task<TreeGrp> I_OSMInterface.ReadOSMNodeWayRelationAsync(string fileLink)
        {
            treeGrp.lat.Clear();
            treeGrp.lng.Clear();

            treeGrp.treeDefineByColor = false;

            OnlineMapsOSMAPIQuery.ParseOSMResponse(fileLink, out nodes, out ways, out relations);

            await Task.Factory.StartNew(FindRelationTree);
            await Task.Factory.StartNew(FindWayTree);
            await Task.Factory.StartNew(FindNodeTree);

            return treeGrp;
        }

        #region Populate tree using OSM Data(node, way and relation)
        /// <summary>
        /// way get node id. Node id to get lon and lat
        /// </summary>
        private void FindWayTree()
        {

            foreach (OnlineMapsOSMWay way in ways)
            {
                foreach (OnlineMapsOSMTag tagWay in way.tags)
                {
                    
                    if (tagWay.value == "forest" || tagWay.value == "orchard" || tagWay.value == "cemetery" || tagWay.value == "park" || tagWay.value == "golf_course" || tagWay.value == "wood")
                    {
                        List<Vector2> areaLongtitudeLatitude = new List<Vector2>();

                        var nodeID = way.GetNodes(nodes);
                        foreach (var eachNodeID in nodeID)
                        {
                            Vector2 combineLonLat = new Vector2((float)eachNodeID.lon, (float)eachNodeID.lat);
                            areaLongtitudeLatitude.Add(combineLonLat);
                            if (IsMarkerInBoundaries_Circle(eachNodeID.lon, eachNodeID.lat))
                            {
                                treeGrp.lng.Add(eachNodeID.lon);
                                treeGrp.lat.Add(eachNodeID.lat);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Node id to get lon and lat
        /// </summary>
        private void FindNodeTree()
        {
            foreach (OnlineMapsOSMNode node in nodes)
            {
                foreach (OnlineMapsOSMTag tagNode in node.tags)
                {
                    if (tagNode.value == "tree")
                    {
                        if (IsMarkerInBoundaries_Circle(node.lon, node.lat))
                        {
                            treeGrp.lng.Add(node.lon);
                            treeGrp.lat.Add(node.lat);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Relation to get way reference id. way get node id. Node id to get lon and lat
        /// </summary>
        private void FindRelationTree()
        {
            List<string> relationMembers = new List<string>();

            foreach (OnlineMapsOSMRelation relation in relations)
            {
                foreach (OnlineMapsOSMTag tagRelation in relation.tags)
                {
                    if (tagRelation.value == "forest" || tagRelation.value == "orchard" || tagRelation.value == "cemetery" || tagRelation.value == "park" || tagRelation.value == "golf_course" || tagRelation.value == "wood")
                    {
                        List<Vector2> areaLongtitudeLatitude = new List<Vector2>();

                        foreach (OnlineMapsOSMRelationMember memberRel in relation.members)
                        {
                            foreach (OnlineMapsOSMWay way in ways)
                            {
                                if (way.id.ToString() == memberRel.reference)
                                {
                                    var nodeID = way.GetNodes(nodes);
                                    //CheckID(nodeID);
                                    foreach (var eachNodeID in nodeID)
                                    {
                                        Vector2 combineLonLat = new Vector2((float)eachNodeID.lon, (float)eachNodeID.lat);
                                        areaLongtitudeLatitude.Add(combineLonLat);
                                        if(IsMarkerInBoundaries_Circle(eachNodeID.lon, eachNodeID.lat))
                                        {
                                            treeGrp.lng.Add(eachNodeID.lon);
                                            treeGrp.lat.Add(eachNodeID.lat);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion Populate tree using OSM Data(node, way and relation)

        #region Function to check Longitiude and latitude within the circle boundary
        Vector2 Invert(Vector2 amt)
        {
            return new Vector3(1.0f / amt.x, 1.0f / amt.y);
        }

        Vector2 ToLocalCoordinate(Vector2 min, Vector2 max, Vector2 target)
        {
            //bound.center = origin(0,0,0)
            Vector2 center = (min + max) / 2;
            var result = center - target;
            var scaleAmt = Invert(max - min);
            result.Scale(scaleAmt);
            return result;
        }

        bool IsMarkerInBoundaries_Circle(double lngChecking, double latChecking)
        {
            Vector2 lngLatChecking = new Vector2((float)lngChecking, (float)latChecking);
            var result = ToLocalCoordinate(bottomRightXBottomRightY, topLeftXTopLeftY, lngLatChecking);
            return (result.sqrMagnitude < radiusSquared);
        }
        #endregion  Function to check Longitiude and latitude within the circle
    }
}
