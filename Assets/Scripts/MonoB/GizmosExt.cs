using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosExt : MonoBehaviour
{

    public enum ExtMode
    {
        Area,
        Raycast,
    }

    public ExtMode ShowMode = ExtMode.Area;

    public float Width = 0.5f;
    public float Depth = 0.6f;
    public float Height = 0.0f;

    public Color ShowColor = Color.red;

    public bool ShowGridPoint = false;
    public int GridCnt = 5;
   

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (ShowMode == ExtMode.Area)
        {
            Gizmos.color = ShowColor;
            Gizmos.DrawCube(transform.position, new Vector3(Width, Height, Depth));
            //
            if (ShowGridPoint)
            {
                Gizmos.color = Color.red;
                float y = transform.position.y;
                float left = transform.position.x + Width / 2.0f;
                float back = transform.position.z - Depth / 2.0f;
                for (int i = 0; i < GridCnt + 1; i++)
                {
                    float z = back + (Depth / GridCnt) * i;
                    for (int j = 0; j < GridCnt + 1; j++)
                    {
                        
                        float x = left - (Width / GridCnt) * j;
                        if(x>0 && z > 0)
                        {
                            continue;
                        }
                        Gizmos.DrawSphere(new Vector3(x, y, z), 0.01f);
                    }
                }
            }
            //
            return;
        }

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1);

        //Gizmos.DrawSphere(transform.position + Vector3.down * 0.7f, 0.055f);
    }
#endif


}
