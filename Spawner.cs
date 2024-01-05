using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


enum grassType
{
    Straight,
    CoCentric,
    StagCoCentric
}

public class Spawner : MonoBehaviour
{
    public GameObject grass;

    [SerializeField] private int MaxX;
    [SerializeField] private int MaxY;
    [SerializeField] private grassType gtype;
    [SerializeField] private float CoChange;
    [SerializeField] private Vector2 AnA;
    [SerializeField] private Vector2 AnB;
    [SerializeField] private float A2rad;
    [SerializeField] private float B2rad;
    
    void Start()
    {

        if (gtype == grassType.StagCoCentric)
        {
            Vector3 og = transform.position;
            
            og.y += transform.GetComponent<Collider>().bounds.size.y / 2;

            Vector2 A = new Vector2(-2, -2);
            Vector2 B = new Vector2(-2, 2);

            float startradA = 0.1f;
            float startradB = 0.1f;
            CoChange *= 2.5f;

            for (int i = 0; i <= MaxX; i++)
            {

                float arad = startradA + (float)i * CoChange;
                
                for (int i2 = 0; i2 <= MaxY; i2++)
                {
                    float brad = startradB + (float)i2 * CoChange;
                    brad = brad + (Convert.ToBoolean((i % 3)) ? 0f : 0.4f * CoChange);
                    arad = arad + (Convert.ToBoolean((i2 % 3)) ? 0f : 0.4f * CoChange);
                   
                    /*
                     *CondiƟons:
    If d > r0 + r1 then there are no soluƟons, the circles are separate.
    If d < |r0 - r1| then there are no soluƟons because one circle is contained within the other.
    If d = 0 and r0 = r1 then the circles are coincident and there are an infinite number of soluƟons.
                     *
                     *
                     */

                    float d = Vector2.Distance(B, A);
                    if (d > arad + brad || d < Mathf.Abs(arad - brad) || d == 0 && arad == brad)
                    {

                        continue;
                    }

                    float a = (arad * arad - brad * brad + d * d) / (2 * d);

                    float h = Mathf.Sqrt(arad * arad - a * a);

                    Vector2 p2 = A + ((B - A) / d) * a;

                    Vector2 p3p = new Vector2((float)(p2.x + ((B - A) / d).y * h), (float)(p2.y - ((B - A) / d).x * h));
                    Vector2 p3n = new Vector2((float)(p2.x - ((B - A) / d).y * h), (float)(p2.y + ((B - A) / d).x * h));

                   

                    Vector3 pos = new Vector4(p3p.x,
                        0,
                        p3p.y);

                    if (pos.x <= 1 && pos.x >= -1 && pos.z <= 1 && pos.z >= -1)
                    {
                        print("B: " + pos);
                        pos = new Vector3(pos.x * transform.GetComponent<Collider>().bounds.size.x / 2, 0,
                            pos.z * transform.GetComponent<Collider>().bounds.size.z / 2);
                        print(pos);
                        grass = Instantiate(grass, og + pos, transform.rotation);
                        grass.transform.position += new Vector3(0, grass.GetComponent<Collider>().bounds.size.y / 2, 0);



                    }


                  

                    Vector3 pos2 = new Vector4(p3n.x,
                        0,
                        p3n.y);

                    if (pos2.x <= 1 && pos2.x >= -1 && pos2.z <= 1 && pos2.z >= -1)
                    {
                        if (d == (arad + brad))
                        {
                            continue;
                        }

                        print("B: " + pos2);
                        pos2 = new Vector3(pos2.x * transform.GetComponent<Collider>().bounds.size.x / 2, 0,
                            pos2.z * transform.GetComponent<Collider>().bounds.size.z / 2);

                        grass = Instantiate(grass, og + pos2, transform.rotation);
                        grass.transform.position += new Vector3(0, grass.GetComponent<Collider>().bounds.size.y / 2, 0);

                        print(pos2);
                    }







                }
            }
        }
        else if (gtype == grassType.CoCentric)
        {
            Vector3 og = transform.position;
           
            og.y += transform.GetComponent<Collider>().bounds.size.y / 2;

            Vector2 A = new Vector2(-2, -2);
            Vector2 B = new Vector2(-2, 2);

            float startradA = 0.1f;
            float startradB = 0.1f;
            CoChange *= 2.5f;

            for (int i = 0; i <= MaxX; i++)
            {

                float arad = startradA + (float)i * CoChange;
                
                for (int i2 = 0; i2 <= MaxY; i2++)
                {
                    float brad = startradB + (float)i2 * CoChange;
                   
                    /*
                     *CondiƟons:
    If d > r0 + r1 then there are no soluƟons, the circles are separate.
    If d < |r0 - r1| then there are no soluƟons because one circle is contained within the other.
    If d = 0 and r0 = r1 then the circles are coincident and there are an infinite number of soluƟons.
                     *
                     *
                     */

                    float d = Vector2.Distance(B, A);
                    if (d > arad + brad || d < Mathf.Abs(arad - brad) || d == 0 && arad == brad)
                    {

                        continue;
                    }

                    float a = (arad * arad - brad * brad + d * d) / (2 * d);

                   
                    float h = Mathf.Sqrt(arad * arad - a * a);

                    Vector2 p2 = A + ((B - A) / d) * a;

                    Vector2 p3p = new Vector2((float)(p2.x + ((B - A) / d).y * h), (float)(p2.y - ((B - A) / d).x * h));
                    Vector2 p3n = new Vector2((float)(p2.x - ((B - A) / d).y * h), (float)(p2.y + ((B - A) / d).x * h));

                  

                    Vector3 pos = new Vector4(p3p.x,
                        0,
                        p3p.y);

                    if (pos.x <= 1 && pos.x >= -1 && pos.z <= 1 && pos.z >= -1)
                    {
                        print("B: " + pos);
                        pos = new Vector3(pos.x * transform.GetComponent<Collider>().bounds.size.x / 2, 0,
                            pos.z * transform.GetComponent<Collider>().bounds.size.z / 2);
                        print(pos);
                        grass = Instantiate(grass, og + pos, transform.rotation);
                        grass.transform.position += new Vector3(0, grass.GetComponent<Collider>().bounds.size.y / 2, 0);



                    }


                    

                    Vector3 pos2 = new Vector4(p3n.x,
                        0,
                        p3n.y);

                    if (pos2.x <= 1 && pos2.x >= -1 && pos2.z <= 1 && pos2.z >= -1)
                    {
                        if (d == (arad + brad))
                        {
                            continue;
                        }

                        print("B: " + pos2);
                        pos2 = new Vector3(pos2.x * transform.GetComponent<Collider>().bounds.size.x / 2, 0,
                            pos2.z * transform.GetComponent<Collider>().bounds.size.z / 2);

                        grass = Instantiate(grass, og + pos2, transform.rotation);
                        grass.transform.position += new Vector3(0, grass.GetComponent<Collider>().bounds.size.y / 2, 0);

                        print(pos2);
                    }


                    





                }
            }
        }
        else if (gtype == grassType.Straight)
        {
            Vector3 og = transform.position;
             og.x -= transform.GetComponent<Collider>().bounds.size.x / 2;
            og.z -= transform.GetComponent<Collider>().bounds.size.z / 2;
            og.y += transform.GetComponent<Collider>().bounds.size.y / 2;


            for (int i = 0; i <= MaxX; i++)
            {



                for (int i2 = 0; i2 <= MaxY; i2++)
                {



                    Vector3 pos = new Vector3(transform.GetComponent<Collider>().bounds.size.x * i / MaxX,
                        0,
                        transform.GetComponent<Collider>().bounds.size.z * i2 / MaxY);


                    print(pos);
                    grass = Instantiate(grass, og + pos, transform.rotation);
                    grass.transform.position += new Vector3(0, grass.GetComponent<Collider>().bounds.size.y / 2, 0);






                 


                }


            }
        }
    }
}