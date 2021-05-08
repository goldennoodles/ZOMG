using UnityEngine;
using UnityEngine.Serialization;

namespace world
{
    public class WorldGenerator : MonoBehaviour
    {
        
        [FormerlySerializedAs("WorldX")] public int worldX = 20;
        [FormerlySerializedAs("WorldZ")] public int worldZ = 20;

        public GameObject floorObject;
        void Awake()
        {
            
            GameObject cellHolder = new GameObject();
                cellHolder.name = "CellHolder";
                
            for (int x = -worldX, i = 0; x < worldX; x++, i++)
            {
                for (int z = -worldZ; z < worldZ; z++)
                {
                    Vector3 pos = new Vector3(
                        x: x * 1.05f,
                        y: NoiseGeneration(x, z, 2f),
                        z: z * 1.05f
                    );
                    
                    GameObject a = Instantiate(floorObject, pos, Quaternion.identity);
                        a.name = "Cell: " + pos;
                        a.transform.SetParent(cellHolder.transform);
                }
            }
        }

        private float NoiseGeneration(int x, int z, float detailScale)
        {
            return Mathf.PerlinNoise(x * 2, z * .5f) / detailScale;
        }
    }
}