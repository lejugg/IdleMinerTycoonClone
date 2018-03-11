using IdleMinerTycoonClone.GameLogic.Jobs;
using IdleMinerTycoonClone.GameLogic.Workers;
using System;
using UnityEngine;

namespace IdleMinerTycoonClone.GameLogic.Tasks
{

	/// <summary>
	/// Makes a worker move to a storage.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	[CreateAssetMenu (fileName = "Move To Storage Task")]
	public class MoveToStorageTask : AbstractTask
	{
		#region implement abstract task

		/// <summary>
		/// Instantiates this instance.
		/// </summary>
		/// <returns></returns>
		public override AbstractTask Instantiate ()
		{
			return CreateInstance<MoveToStorageTask> ();
		}

		/// <summary>
		/// This
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="onComplete"></param>
		public override void DoTask (WorkerController controller, IStorage storage, Action onComplete)
		{
			var storagePosition = storage.GetPosition ();
			//Debug.Log ("Task: Move to " + storagePosition);			
			controller.Agent.MoveTo (storagePosition, onComplete);			
		}

		#endregion

	}
}
