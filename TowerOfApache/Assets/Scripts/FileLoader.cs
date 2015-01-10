using UnityEngine;
using System.Collections;
using System.IO;

public class FileLoader : MonoBehaviour {
	static TextAsset file;
	static StringReader reader;

	public static int[,] readTextAsInt(string path){
		file = Resources.Load (@path) as TextAsset;
		reader = new StringReader(file.text);
		
		string elements = reader.ReadLine();	//一列読み込み
		string[] e = elements.Split(',');		//,区切り
		
		//1行目に配列の要素数の情報を入れる
		int tate = int.Parse(e[0]);
		int yoko = int.Parse(e[1]);
		
		int[,] result = new int[tate,yoko];
		
		int i = 0; 	//雑用変数
		while (reader.Peek() > -1) {
			elements = reader.ReadLine();	//一列読み込み
			e = elements.Split(',');	//,区切り
			for(int j=0; j<e.Length;j++){	//一列分を処理
				result[i,j] = int.Parse(e[j]);
			}
			i++;
		}
		
		reader.Dispose();
		reader.Close();

		return result;
	}

	public static float[,] readTextAsFloat(string path){
		file = Resources.Load (@path) as TextAsset;
		reader = new StringReader(file.text);
		
		string elements = reader.ReadLine();	//一列読み込み
		string[] e = elements.Split(',');		//,区切り
		
		//1行目に配列の要素数の情報を入れる
		int tate = int.Parse(e[0]);
		int yoko = int.Parse(e[1]);
		
		float[,] result = new float[tate,yoko];
		
		int i = 0; 	//雑用変数
		while (reader.Peek() > -1) {
			elements = reader.ReadLine();	//一列読み込み
			e = elements.Split(',');	//,区切り
			for(int j=0; j<e.Length;j++){	//一列分を処理
				result[i,j] = float.Parse(e[j]);
			}
			i++;
		}
		
		reader.Dispose();
		reader.Close();
		
		return result;
	}

	public static double[,] readTextAsDouble(string path){
		file = Resources.Load (@path) as TextAsset;
		reader = new StringReader(file.text);
		
		string elements = reader.ReadLine();	//一列読み込み
		string[] e = elements.Split(',');		//,区切り
		
		//1行目に配列の要素数の情報を入れる
		int tate = int.Parse(e[0]);
		int yoko = int.Parse(e[1]);
		
		double[,] result = new double[tate,yoko];
		
		int i = 0; 	//雑用変数
		while (reader.Peek() > -1) {
			elements = reader.ReadLine();	//一列読み込み
			e = elements.Split(',');	//,区切り
			for(int j=0; j<e.Length;j++){	//一列分を処理
				result[i,j] = double.Parse(e[j]);
			}
			i++;
		}
		
		reader.Dispose();
		reader.Close();
		
		return result;
	}

	public static string[,] readTextFileAsString(string path){
		file = Resources.Load (@path) as TextAsset;
		reader = new StringReader(file.text);
		
		string elements = reader.ReadLine();	//一列読み込み
		string[] e = elements.Split(',');		//,区切り
		
		//1行目に配列の要素数の情報を入れる
		int tate = int.Parse(e[0]);
		int yoko = int.Parse(e[1]);
		
		string[,] result = new string[tate,yoko];
		
		int i = 0; 	//雑用変数
		while (reader.Peek() > -1) {
			elements = reader.ReadLine();	//一列読み込み
			e = elements.Split(',');	//,区切り
			for(int j=0; j<e.Length;j++){	//一列分を処理
				result[i,j] = e[j];
			}
			i++;
		}
		
		reader.Dispose();
		reader.Close();
		
		return result;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
