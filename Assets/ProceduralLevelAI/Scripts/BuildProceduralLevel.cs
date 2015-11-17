using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildProceduralLevel : MonoBehaviour {

	public Transform startPos;
	public Transform endPos;

	List<Vector3> vertices = new List<Vector3>();
	List<Vector2> uvs = new List<Vector2>();
	List<int> triangles = new List<int>();

	void Start () {
	
		// create basic mesh floor
		BuildFloor();

		// build navmesh
		var rcdtcs = GetComponent<RcdtcsSampleSoloMeshComponent> ();
		rcdtcs.RecomputeSystem ();

		// compute path
		rcdtcs.m_StartPos = startPos.position;
		rcdtcs.m_EndPos = endPos.position;
		rcdtcs.RecomputePath();

	}

	void BuildFloor()
	{
		var mesh = GetComponent<MeshFilter>().mesh;
		var index = 0;

		// first path
		vertices.Add(new Vector3(0,0,0));
		vertices.Add(new Vector3(0,0,5));
		vertices.Add(new Vector3(1,0,5));
		vertices.Add(new Vector3(1,0,0));
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));

		// turn right
		vertices.Add(new Vector3(1,0,4));
		vertices.Add(new Vector3(1,0,5));
		vertices.Add(new Vector3(5,0,5));
		vertices.Add(new Vector3(5,0,4));
		index = vertices.Count-4;
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));

		// right up
		vertices.Add(new Vector3(4,0,5));
		vertices.Add(new Vector3(4,0,10));
		vertices.Add(new Vector3(5,0,10));
		vertices.Add(new Vector3(5,0,5));
		index = vertices.Count-4;
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));


		// turn left
		vertices.Add(new Vector3(-5,0,4));
		vertices.Add(new Vector3(-5,0,5));
		vertices.Add(new Vector3(0,0,5));
		vertices.Add(new Vector3(0,0,4));
		index = vertices.Count-4;
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));

		// left up
		vertices.Add(new Vector3(-4,0,5));
		vertices.Add(new Vector3(-5,0,5));
		vertices.Add(new Vector3(-5,0,10));
		vertices.Add(new Vector3(-4,0,10));
		index = vertices.Count-4;
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));

		// connect end
		vertices.Add(new Vector3(-5,0,9));
		vertices.Add(new Vector3(-5,0,10));
		vertices.Add(new Vector3(4,0,10));
		vertices.Add(new Vector3(5,0,9));
		index = vertices.Count-4;
		triangles.Add (index+0);
		triangles.Add (index+1);
		triangles.Add (index+2);
		triangles.Add (index+0);
		triangles.Add (index+2);
		triangles.Add (index+3);
		uvs.Add (new Vector2 (0, 0));
		uvs.Add (new Vector2 (0, 1));
		uvs.Add (new Vector2 (1, 1));
		uvs.Add (new Vector2 (1, 0));

		// mesh above is too small for default values, so just scaling to get it working
		var meshScale = 5;
		for (int i = 0; i < vertices.Count; i++) {
			vertices[i] *= meshScale;
		}

		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		mesh.Optimize ();

		GetComponent<MeshCollider>().sharedMesh = null;
		GetComponent<MeshCollider>().sharedMesh = mesh;
	}

}
