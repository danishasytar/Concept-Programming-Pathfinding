using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	// Use this for initialization
	private UnityEngine.AI.NavMeshAgent agent;
	public GameObject[] waypoints;
	//Animator anim;
	float rotSpeed = 0.8f;
	float speed = 0.8f;
	float accuracyWP = 0.5f;
	int currentWP = 0;

	List<Transform> path = new List<Transform>();

	void Start () {
		
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.autoBraking = false;
		//anim = GetComponent<Animator>();
		foreach (GameObject go in waypoints)
		{
			path.Add(go.transform);
		}
		currentWP= FindClosestWP();
		//anim.SetBool("isWalking", true);
	}

	int FindClosestWP()
	{
		if (path.Count == 0) {

			return -1;
		}
		int closest = 0;
		float lastDist = Vector3.Distance(this.transform.position, path[0].position);
		for(int i = 1; i < path.Count; i++)
		{
			float thisDist = Vector3.Distance(this.transform.position, path[i].position);
			if(lastDist > thisDist && i != currentWP)
			{
				closest = i;

			}
			agent.destination = path[closest].position;

		}


		return closest;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 direction = path[currentWP].position - transform.position;
		this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
		this.transform.Translate(0, 0, Time.deltaTime * speed);
		if(direction.magnitude < accuracyWP)
		{
			
			path.Remove(path[currentWP]);
			currentWP = FindClosestWP();

		}
		
	}
}
