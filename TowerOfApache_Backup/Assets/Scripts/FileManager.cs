using UnityEngine;
using System.Collections;
using System.IO;

//csvファイルを読み込むために使用する
public class FileManager{
	static FileInfo fi;
	static StreamReader sr;
	static TextAsset csv;
	static StringReader reader;

	/*public static string[,] readFile(string path){
		fi = new FileInfo (@path);	//パスの指定

		//ファイルが存在するか確認
		if (!fi.Exists) {
			Debug.LogError("Dungeon FIle does not exist");
		}

		sr = new StreamReader(fi.OpenRead());	//リーダーのオープン

		string elements = sr.ReadLine();	//一列読み込み
		string[] e = elements.Split(',');	//,区切り

		//1行目に配列の要素数の情報を入れる
		int tate = int.Parse(e[0]);
		int yoko = int.Parse(e[1]);

		string[,] result = new string[tate,yoko];

		int i = 0; 	//雑用変数
		do{
			elements = sr.ReadLine();	//一列読み込み
			e = elements.Split(',');	//,区切り
			for(int j=0; j<e.Length;j++){	//一列分を処理
				result[i,j] = e[j];
			}
			i++;
		}while(!sr.EndOfStream);
		
		sr.Close ();

		return result;
	}*/

	public static string[,] readTextFile(string path){
		csv = Resources.Load (@path) as TextAsset;
		reader = new StringReader(csv.text);

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
}
