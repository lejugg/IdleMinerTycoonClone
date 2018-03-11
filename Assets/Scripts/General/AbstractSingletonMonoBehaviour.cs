using UnityEngine;
using System.Collections;

namespace IdleMinerTycoonClone.General
{
	/// <summary>
	/// Any singleton monobehaviour just exists once in the scene.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	public abstract class AbstractSingletonMonoBehaviour : MonoBehaviour
	{
		#region private fields

		/// <summary>
		/// The instance.
		/// </summary>
		private static AbstractSingletonMonoBehaviour instance;

		#endregion

		#region properties

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>
		/// The instance.
		/// </value>
		public static AbstractSingletonMonoBehaviour Instance
		{
			get
			{
				return instance;
			}
		}

		#endregion

		#region unity function

		/// <summary>
		/// Awakes this instance.
		/// </summary>
		private void Awake ()
		{
			if (instance != null && instance != this)
			{
				Destroy (this.gameObject);
			}
			else
			{
				instance = this;
			}
		}

		#endregion
	}


}
