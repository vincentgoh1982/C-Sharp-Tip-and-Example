
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static TreeGenerateInNodeWayRelation.I_OSMTree;

namespace TreeGenerateInNodeWayRelation
{

    public class I_ColorTileTree : I_OSMInterface
    {
        public TreeGrp treeGrp = new TreeGrp();

        /// <summary>
        /// Temporary storage for comparing the value
        /// </summary>

        private Vector2 topLeftXTopLeftY;
        private Vector2 bottomRightXBottomRightY;
        private float radiusSquared;
        private int isSameNumber = 0;

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

            treeGrp.treeDefineByColor = true;

            await Task.Factory.StartNew(CreateRandomForestInside);
            return treeGrp;
        }
        #region Function to populate trees within the circle
        private void CreateRandomForestInside()
        {

            float treeDistRange = 0.0005f;
            float rangeHortizontal = topLeftXTopLeftY.x - bottomRightXBottomRightY.x;
            float numTreeLongtitude = rangeHortizontal / treeDistRange;
            float minLatitude = bottomRightXBottomRightY.y;
            float minLongititude = bottomRightXBottomRightY.x;

            EvenlyTree(numTreeLongtitude, minLatitude, minLongititude);
            OddTree(numTreeLongtitude, minLatitude, minLongititude);
        }

        private void EvenlyTree(float numTreeLongtitude, float minLatitude, float minLongititude)
        {
            float newLon = minLongititude;

            for (int treeLon = 0; treeLon < (int)numTreeLongtitude; treeLon++)
            {
                newLon += 0.001f;
                float newLat = minLatitude;
                for (int treeLat = 0; treeLat < (int)numTreeLongtitude; treeLat++)
                {
                    newLat -= 0.001f;

                    if (IsMarkerInBoundaries_Circle(newLon, newLat))
                    {
                        treeGrp.lng.Add(newLon);
                        treeGrp.lat.Add(newLat);
                    }
                }
            }
        }

        private void OddTree(float numTreeLongtitude, float minLatitude, float minLongititude)
        {
            float newLon = minLongititude + 0.0005f;

            for (int treeLon = 0; treeLon < (int)numTreeLongtitude; treeLon++)
            {
                newLon += 0.001f;
                float newLat = minLatitude - 0.0005f; ;
                for (int treeLat = 0; treeLat < (int)numTreeLongtitude; treeLat++)
                {
                    newLat -= 0.001f;

                    if (IsMarkerInBoundaries_Circle(newLon, newLat))
                    {
                        treeGrp.lng.Add(newLon);
                        treeGrp.lat.Add(newLat);
                    }
                }
            }
        }
        #endregion  Function to populate trees within the circle

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

