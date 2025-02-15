﻿using UnityEngine;
using System.Collections.Generic;


public class BoidsSimilater : MonoBehaviour {
	// ステージ領域
	public const float MinX = -50.0f;
	public const float MaxX = 50.0f;
	public const float MinY = 0.0f;
	public const float MaxY = 50.0f;
	public const float MinZ = -50.0f;
	public const float MaxZ = 50.0f;

	public GameObject Boid;  // boidプレハブ

	
	// 壁となるPlane
	private Plane Up = new Plane(new Vector3(0,-1,0), new Vector3(0,MaxY,0));
	private Plane Down = new Plane(new Vector3(0,1,0), new Vector3(0,MinY,0));
	private Plane Left = new Plane(new Vector3(1,0,0), new Vector3(MinX,0,0));
	private Plane Right = new Plane(new Vector3(-1,0,0), new Vector3(MaxX,0,0));
	private Plane Forward = new Plane(new Vector3(0,0,-1), new Vector3(0,0,MaxZ));
	private Plane Back = new Plane(new Vector3(0,0,1), new Vector3(0,0,MinZ));

	public int BoidCount = 12;  // boidの数
	private List<BoidScript> boids = new List<BoidScript>();

	public const float BoidMaxV = 8.0f;  // boidが出せる最高速度
	public const float BoidMinV = 6.0f;  // boidが出せる最低速度
	public const float BoidFOV = 25.0f;  // boidの視界

	// シングルトンアクセス
	private static BoidsSimilater instance;
	public static BoidsSimilater Instance
	{
		get {
			if (instance == null) {
				instance = GameObject.FindObjectOfType<BoidsSimilater>();
				DontDestroyOnLoad(instance.gameObject);
			}
			return instance;
		}
	}

    void Awake () {

    }

    // Use this for initialization
    void Start () {
		for (int i = 0; i < BoidCount; i++)
        {
			GameObject boid = (GameObject) Instantiate (Boid,
				new Vector3 (Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ)),
				Random.rotation);
			boid.transform.SetParent(transform);
			boids.Add (boid.GetComponent<BoidScript> ());
        }
    }


    public void InitPosition()
    {
	    for (int i = 0; i < BoidCount; i++)
	    {
		    boids[i].Position =
			    new Vector3(Random.Range(MinX, MaxX), Random.Range(MinY, MaxY), Random.Range(MinZ, MaxZ));
	    }
    }
    // Update is called once per frame
    void Update () {

	    if (Input.GetKeyDown("b"))
	    {
		    InitPosition();
	    }
    }

	public List<IObject> GetVirtualBoidsOnWall (BoidScript obj) {
		List<IObject> boids = new List<IObject> ();
		float d;
		d = Up.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Up.normal));
		}
		d = Down.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Down.normal));
		}
		d = Left.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Left.normal));
		}
		d = Right.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Right.normal));
		}
		d = Forward.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Forward.normal));
		}
		d = Back.GetDistanceToPoint (obj.transform.localPosition);
		if (d <= BoidFOV) {
			boids.Add (new VirtualBoid(obj.Position - d * Back.normal));
		}
		return boids;
	}

	public List<BoidScript> GetOtherBoidsInFOV (BoidScript obj) {
		List<BoidScript> retBoids = new List<BoidScript> ();
		foreach(BoidScript boid in boids) {
			if (boid == obj)
				continue;
			Vector3 diff = boid.Position - obj.Position;
			if (diff.magnitude <= BoidFOV)
				retBoids.Add (boid);
		}
		return retBoids;
	}
}
