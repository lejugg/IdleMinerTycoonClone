using IdleMinerTycoonClone.GameLogic.Jobs;
using IdleMinerTycoonClone.GameLogic.Workers;
using System;
using System.Collections;
using UnityEngine;

namespace IdleMinerTycoonClone.GameLogic.Tasks
{

	/// <summary>
	/// Makes a worker deposit his temporary balance to a storage.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	[CreateAssetMenu (fileName = "Deposit Task")]
	public class DepositTask : AbstractTask
	{
		#region serialized fields

		/// <summary>
		/// The duration it takes the worker to deposit.
		/// </summary>
		[SerializeField]
		private float duration;

		#endregion

		#region implement abstract task

		/// <summary>
		/// Instantiates this instance.
		/// </summary>
		/// <returns></returns>
		public override AbstractTask Instantiate ()
		{
			var instantiatedTask = CreateInstance<DepositTask> ();
			instantiatedTask.duration = this.duration;
			return instantiatedTask;
		}

		/// <summary>
		/// This Task places all of the workers temporary storage into the given storage.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="onComplete"></param>
		public override void DoTask (WorkerController controller, IStorage storage, Action onComplete)
		{
			//Debug.Log ("Task: Deposit for " + duration + " seconds");
			controller.Agent.StartCoroutine (Depositing(controller, storage, onComplete));
		}

		/// <summary>
		/// Depositings the specified controller.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="storage">The storage.</param>
		/// <returns></returns>
		private IEnumerator Depositing (WorkerController controller, IStorage storage, Action onComplete)
		{
			yield return new WaitForSeconds (duration);

			storage.Deposit (controller.TemporaryStorage.WithdrawAll ());

			onComplete ();
		}

		#endregion

	}
}
