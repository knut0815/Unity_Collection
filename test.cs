using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float radius = 1;
    public Vector2 regionSize = Vector2.one;
    public int rejectionSamples = 30;
    public float displayRadius = 1;

    public GameObject prefTree;
    

    List<Vector2> points;

    void OnValidate()
    {
        points = PoissonDiscSampling.GeneratePoints(radius, regionSize, rejectionSamples);
        //DrawTrees();
    }
    private void Start()
    {
        DrawTrees();
    }
    private void OnDrawGizmos()
    {
        //Gizmos.matrix = transform.localToWorldMatrix;
        Vector3 wire = new Vector3(regionSize.x, 0, regionSize.y);
        Vector3 center = new Vector3(regionSize.x/2, 0 ,regionSize.y/2 );
        Gizmos.DrawWireCube(center, wire);
        

        if (points != null)
        {
            
            foreach (Vector2 point in points)
            {
                Vector3 position = new Vector3(point.x, 0, point.y);
                position.y = Terrain.activeTerrain.SampleHeight(position) + Terrain.activeTerrain.GetPosition().y;
                Gizmos.DrawSphere(position, displayRadius);
            }
        }

    }
    void DrawTrees()
    {
        
        if (points != null)
        {
            foreach (Vector2 point in points)
            {
                Vector3 position = new Vector3(point.x, 0, point.y);
                position.y = Terrain.activeTerrain.SampleHeight(position) + Terrain.activeTerrain.GetPosition().y;
                Instantiate(prefTree, position, Quaternion.identity);
            }
        }

    }
}
