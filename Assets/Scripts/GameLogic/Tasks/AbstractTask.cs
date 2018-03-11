using UnityEngine;
using System;
using IdleMinerTycoonClone.GameLogic.Workers;
using IdleMinerTycoonClone.GameLogic.Jobs;

namespace IdleMinerTycoonClone.GameLogic.Tasks
{

	/// <summary>
	/// A task can be performed by a worker.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	[Serializable]
	public abstract class AbstractTask : ScriptableObject
	{
		#region basic logic

		/// <summary>
		/// This 
		/// </summary>
		public abstract void DoTask (WorkerController controller, IStorage storage, Action onComplete);

		/// <summary>
		/// Instantiates this instance.
		/// </summary>
		/// <returns></returns>
		public abstract AbstractTask Instantiate ();

		#endregion
	}
}
