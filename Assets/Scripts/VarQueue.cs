using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VarQueue : MonoBehaviour {

	private static Dictionary<string, string> stringDic = new Dictionary<string, string> ();
	private static Dictionary<string, bool> boolDic = new Dictionary<string, bool> ();
	private static Dictionary<string, short> shortDic = new Dictionary<string, short> ();
	private static Dictionary<string, int> intDic = new Dictionary<string, int> ();
	private static Dictionary<string, HostData> hostDataDic = new Dictionary<string, HostData> ();

	public static void PushString(string key, string valToPush){
		stringDic.Add(key, valToPush);
	}

	public static string PopString(string key){
		return stringDic[key];
	}

	public static void PushBool(string key, bool valToPush){
		boolDic.Add(key, valToPush);
	}
	
	public static bool PopBool(string key){
		return boolDic[key];
	}

	public static void PushShort(string key, short valToPush){
		shortDic.Add(key, valToPush);
	}
	
	public static short PopShort(string key){
		return shortDic[key];
	}

	public static void PushInt(string key, int valToPush){
		intDic.Add(key, valToPush);
	}
	
	public static int PopInt(string key){
		return intDic[key];
	}

	public static void PushHostData(string key, HostData valToPush){
		hostDataDic.Add(key, valToPush);
	}
	
	public static HostData PopHostData(string key){
		return hostDataDic[key];
	}
	
}
