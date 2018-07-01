using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleMeshVertices : MonoBehaviour {

	// 頂点を探すScript参考
	// http://unitylab.wiki.fc2.com/wiki/%E3%83%A1%E3%83%83%E3%82%B7%E3%83%A5%E3%82%B3%E3%83%B3%E3%83%9D%E3%83%BC%E3%83%8D%E3%83%B3%E3%83%88%E3%81%AE%E9%A0%82%E7%82%B9%E4%BD%8D%E7%BD%AE%EF%BC%88Vector3%E9%85%8D%E5%88%97%EF%BC%89%E3%82%92%E5%BE%97%E3%82%8B

	[Header("各頂点座標をLogで表示(※処理が重くなる)"), SerializeField]
	private bool DebugLog_Vertces = false;

	[Header("Meshのリアルタイム更新")]
	public bool realTime_Update = false;

	// 変形操作する頂点の高さの値
	[Header("realTime_Updateをtrueにし,各値を変更")]
	public float[] vertexHeight_val = new float[3]{1, 1, 1};

	// 1高さの変動値
	public float oneHeight_value = 2;

	// vertices(各面の頂点)のTransform
	private Vector3[] verticesTransform;

	// Meshの形動かす対象
	private MeshFilter mf;
	private MeshCollider m_col;

	void Awake () {
		mf = GetComponent<MeshFilter>();
		m_col = GetComponent<MeshCollider>();

		// vertices(各面の頂点)のTransform
		verticesTransform = mf.mesh.vertices;

		for(int i = 0; i < vertexHeight_val.Length; i++){
			vertexHeight_val[i] *= oneHeight_value;
		}
		Set_triVertices();
	}

	void Start(){
		if (DebugLog_Vertces == true) {
			for (int i = 0; i < mf.mesh.vertices.Length; i++) {
				Debug.Log ("!!_@ " + i + mf.mesh.vertices [i]);
			}
		}
	}

	public void Update_TriangleMesh(){
		// Meshプロパティのboundsを更新
		mf.mesh.vertices = verticesTransform;
		mf.mesh.RecalculateBounds();
		// MeshColliderの更新
		m_col.sharedMesh = mf.mesh;
	}

	// 真上からみて三角形の左上の頂点操作(左)
	public void vertices1(){
		// OK
		verticesTransform[5].y = vertexHeight_val[0];
		verticesTransform[8].y = vertexHeight_val[0];
		verticesTransform[15].y = vertexHeight_val[0];
	}
	// 真上からみて三角形の右上の頂点操作(右)
	public void vertices2(){
		verticesTransform[1].y = vertexHeight_val[1];
		verticesTransform[4].y = vertexHeight_val[1];
		verticesTransform[16].y = vertexHeight_val[1];
	}
	// 真上からみて三角形の左下の頂点操作(手前)
	public void vertices3(){
		// z
		verticesTransform[0].y = vertexHeight_val[2];
		verticesTransform[9].y = vertexHeight_val[2];
		verticesTransform[17].y = vertexHeight_val[2];
	}

	public void Set_triVertices(){
		// vertices-()は,引数で実行したい
		vertices1();
		vertices2();
		vertices3();
		// Mesh情報を更新
		Update_TriangleMesh ();
	}

	void Update() {
		if(realTime_Update == true){
			Set_triVertices();
		}
	}
}
