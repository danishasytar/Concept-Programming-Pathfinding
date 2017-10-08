using UnityEngine;
using System.Collections;


public class route : MonoBehaviour {

	public Transform[] points;
	private int destPoint = 0;
	private UnityEngine.AI.NavMeshAgent agent;


	void Start () {
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

		agent.autoBraking = false;

		GotoNextPoint();
	}


	void GotoNextPoint() {

		if (points.Length == 0)
			return;

	
		agent.destination = points[destPoint].position;


		destPoint = (destPoint + 1) % points.Length;
	}


	void Update () {

		if (!agent.pathPending && agent.remainingDistance < 0.5f)
			GotoNextPoint();
	}
}