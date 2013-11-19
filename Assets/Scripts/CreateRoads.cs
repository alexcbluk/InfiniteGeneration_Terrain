using UnityEngine;
using System.Collections;

public class CreateRoads : MonoBehaviour {
	
	public Material matRoad;
	public GameObject roadObject;
	
	public float roadHeight;
	
	public float meshWidth;			//Change this number base on your segemented road.
	public float meshHeight;
	public float roadScale;
	
	// Use this for initialization
	void Start () {
		//roadHeight = 0.01f;			//Slightly above the terrain ground
		//meshWidth = 5;
	 	//meshHeight = 1;
	}
	
	// Update is called once per frame
	void Update () {
		checkMouseClick();
	}
	
	void checkMouseClick(){
		if(Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Debug.Log (ray);
			
			RaycastHit hit = new RaycastHit();
			//If the collider shoots a raycast and it hit an object (Our terrain) then create the road
			
			
			if(Physics.Raycast(ray, out hit, Mathf.Infinity) ) {
				RoadCreation(hit.point);	//Raycast.point is the actual vector3 position
			}
		
			
		}
	}
	
	
	private void RoadCreation(Vector3 roadStart){
		if(Input.GetMouseButtonDown(0)){
			GameObject road = new GameObject("Road",typeof(MeshFilter), typeof(MeshRenderer) );
			//road.transform.position = new Vector3(0,0.1f,0);
			road.transform.position = roadStart + new Vector3(0, roadHeight, 0);	//Slightly off the terrain
			road.transform.localScale += new Vector3(roadScale,roadScale,roadScale);
			
			
			
			
			//Initalise your Quad Vertices
			Vector3[] vertices = {
				//If we want our road to be 3D with some depth, add 1 to the Y Axis
				new Vector3(0			,0	,meshHeight/2),	//Top left
				new Vector3(meshWidth	,0	,meshHeight/2),	//Top Right
				new Vector3(meshWidth	,0	,-meshHeight/2),//Bottom Right
				new Vector3(0			,0	,-meshHeight/2)	//Bottom Left
			};
			
			int[] triangles = {
				0,1,2,			//Triangle 1
				2,3,0			//Triangle 2
			};
			
			//UV Mapping
			
			Vector2[] uv = {
				new Vector2(0			,0),	
				new Vector2(meshWidth	,0),	
				new Vector2(meshWidth	,meshHeight),	
				new Vector2(0			,meshHeight)
			
			};
			
			Vector3[] normals = {
				Vector3.up,
				Vector3.up,
				Vector3.up,
				Vector3.up
			};
			
			Mesh mesh = new Mesh();
			
			mesh.vertices = vertices;
			mesh.triangles = triangles;
			mesh.uv = uv;					//Apply the texture onto the game object
			mesh.normals = normals;			//Apply the normals for the texture
			
			//Initialise the Mesh Filter and Mesh Render component that we are going to modified
			MeshFilter meshFilter = road.GetComponent<MeshFilter>();
			meshFilter.mesh = mesh;
			MeshRenderer meshRender = road.GetComponent<MeshRenderer>();
			meshRender.material = matRoad;
			//meshRender.sharedMaterial.mainTextureScale.x = 
			meshRender.castShadows = false;		//Since the mesh is slightly above the ground, it may cast shadow so lets turn it off
			
			
		}	
	}
}
