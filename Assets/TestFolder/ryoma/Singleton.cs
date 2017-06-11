using UnityEngine;
using System.Collections;

public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T m_instance = null;
	public static T Instance
	{
		get
		{
			if (m_instance == null)
			{
				System.Type _type = typeof(T);
				m_instance = FindObjectOfType(_type) as T;
#if DEBUG
				if (m_instance == null)
				{
					Debug.LogError(_type.ToString() + "is nothing");
				}
#endif
			}

			return m_instance;
		}
	}
}


//public class Player : SingletonMonoBehaviour<Player>
//{
//	// initilization
//	public void Awake()
//	{
//		if (this != Instance)
//		{
//			Debug.Log("Destroy SingletonInstance");
//			Destroy(this.gameObject);
//			return;
//		}

//		DontDestroyOnLoad(this.gameObject);
//	}

//	public float speed = 4f;
//}