using UnityEngine;

namespace world
{
    public class WorldGenerator : MonoBehaviour
    {
        
        private const int WorldX = 20;
        private const int WorldZ = 20;

        public GameObject floorObject;
        void Awake()
        {
            
            GameObject cellHolder = new GameObject();
                cellHolder.name = "CellHolder";
                
            for (int x = -WorldX, i = 0; x < WorldX; x++, i++)
            {
                for (int z = -WorldZ; z < WorldZ; z++)
                {
                    Vector3 pos = new Vector3(
                        x: x * 1.05f,
                        y: noiseGeneration(x, z, 2f),
                        z: z * 1.05f
                    );
                    
                    GameObject a = Instantiate(floorObject, pos, Quaternion.identity);
                        a.name = "Cell: " + pos;
                        a.transform.SetParent(cellHolder.transform);
                }
            }
        }

        private float noiseGeneration(int x, int z, float detailScale)
        {
            return Mathf.PerlinNoise(x * 2, z * .5f) / detailScale;
        }
    }
}