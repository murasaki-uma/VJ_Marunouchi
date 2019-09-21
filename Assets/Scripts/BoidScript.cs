using UnityEngine;
using System.Collections.Generic;

public interface IObject
{
	Vector3 Position { get; set; }
}

public class VirtualBoid : IObject {
	private Vector3 position;
	public Vector3 Position
	{
		get
		{
			return position;
		}
		set
		{
			position = value;
		}
	}

	public VirtualBoid(Vector3 pos) {
		Position = pos;
	}
}


public class BoidScript : MonoBehaviour, IObject {
	private Vector3 position;  // 位置
	public Vector3 Position
	{
		get
		{
			return position;
		}
		set
		{
			position = transform.localPosition = value;
		}
	}
	private Vector3 velocity;  // 速度
	public Vector3 Velocity
	{
		get
		{
			return velocity;
		}
		set
		{
			velocity = value;
			transform.forward = velocity.normalized;
		}
	}
	private Vector3 acceleration = Vector3.zero;  // 加速度

	public const float Rule1Factor = 5.0f;  // ルール#1の重み
	public const float Rule2Factor = 2.0f;  // ルール#2の重み
	public const float Rule3Factor = 1.0f;  // ルール#3の重み

	void Awake () {
		position = transform.localPosition;
		Velocity = transform.forward * 2.0f;  // 初速
	}

    // Use this for initialization
    void Start () {

    }
		
	// Update is called once per frame
	void Update () {
		// dtと前回の加速度から位置差分・速度を計算
		float dt = Time.deltaTime;
		Vector3 dPos = Velocity * dt + 0.5f * acceleration * dt * dt;	// d = v0*t + 1/2*a*t^2
		Velocity = Velocity + acceleration * dt;	// v = v0 + a*t
		// 速度はBoidMinV以上BoidMaxV以下でなければならない
		float clamped = Mathf.Clamp (Velocity.magnitude, BoidsSimilater.BoidMinV, BoidsSimilater.BoidMaxV);
		Velocity = Velocity / Velocity.magnitude * clamped;
		Position = Position + dPos;

		// 加速度更新
		acceleration = (Rule1Factor * Rule1 () + Rule2Factor * Rule2 () + Rule3Factor * Rule3 ()) / (Rule1Factor + Rule2Factor + Rule3Factor);

		if (Vector3.Distance(Vector3.zero, Position) > 70)
		{
			Position = Vector3.zero;
		}

		if (position.y < -10)
		{
			position = Vector3.zero;
		}
	}

	private Vector3 Rule1 () {
		List<IObject> objects = new List<IObject> ();
		objects.AddRange (BoidsSimilater.Instance.GetVirtualBoidsOnWall (this));
		objects.AddRange (BoidsSimilater.Instance.GetOtherBoidsInFOV (this).ConvertAll<IObject>(c => c as IObject));

		if (objects.Count == 0)
			return Vector3.zero;

		Vector3 vec = Vector3.zero;
		foreach (IObject obj in objects) {
			Vector3 diff = obj.Position - Position;
			vec += -1 * diff.normalized * 10.0f / (diff.magnitude * diff.magnitude);
		}
		return vec / objects.Count;
	}

	private Vector3 Rule2 () {
		List<BoidScript> boids = new List<BoidScript> ();
		boids.AddRange (BoidsSimilater.Instance.GetOtherBoidsInFOV (this));
		
		if (boids.Count == 0)
			return Vector3.zero;

		Vector3 vec = Vector3.zero;
		foreach (BoidScript boid in boids) {
			vec += boid.Velocity;
		}
		Vector3 ave = vec / boids.Count;
		return ave - Velocity;
	}

	private Vector3 Rule3 () {
		List<BoidScript> boids = new List<BoidScript> ();
		boids.AddRange (BoidsSimilater.Instance.GetOtherBoidsInFOV (this));
		
		if (boids.Count == 0)
			return Vector3.zero;
		
		Vector3 pos = Vector3.zero;
		foreach (BoidScript boid in boids) {
			pos += boid.Position;
		}
		Vector3 ave = pos / boids.Count;
		return ave - Position;
	}
}
