using UnityEngine;
using System.Collections;

namespace IdleMinerTycoonClone.General
{
	/// <summary>
	/// Credit to GitHub User timofei7
	/// <a href="https://gist.github.com/timofei7/dae2ce316195208ce8ba">TimoFei7's Github</a>
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		/// <summary>
		/// The instance
		/// </summary>
		private static T _instance = null;

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = (T) FindObjectOfType (typeof (T));
					if (_instance == null)
					{

						string goName = typeof (T).ToString ();

						GameObject go = GameObject.Find (goName);
						if (go == null)
						{
							go = new GameObject ();
							go.name = goName;
						}

						_instance = go.AddComponent<T> ();
					}
				}
				return _instance;
			}
		}

		/// <summary>
		/// Called when [application quit].
		/// </summary>
		public virtual void OnApplicationQuit ()
		{
			// release reference on exit
			_instance = null;
		}

		/// <summary>
		/// Sets the parent.
		/// </summary>
		/// <param name="parentGOName">Name of the parent go.</param>
		protected void SetParent (string parentGOName)
		{
			if (parentGOName != null)
			{
				GameObject parentGO = GameObject.Find (parentGOName);
				if (parentGO == null)
				{
					parentGO = new GameObject ();
					parentGO.name = parentGOName;
				}
				this.transform.parent = parentGO.transform;
			}
		}
	}


}
