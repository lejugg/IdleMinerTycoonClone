using IdleMinerTycoonClone.GameLogic.Jobs;
using IdleMinerTycoonClone.GameLogic.Workers;
using System;
using System.Collections;
using UnityEngine;

namespace IdleMinerTycoonClone.GameLogic.Tasks
{

	/// <summary>
	/// Makes a worker withdraw to temporary balance from a storage.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	[CreateAssetMenu (fileName = "Withdraw Task")]
	public class WithdrawTask : AbstractTask
	{
		#region serialized fields

		/// <summary>
		/// The duration it takes the worker to deposit.
		/// </summary>
		[SerializeField]
		private float duration;

		/// <summary>
		/// The amount that the worker will 
		/// </summary>
		[SerializeField]
		private int amount;

		#endregion

		#region implement abstract task

		/// <summary>
		/// Instantiates this instance.
		/// </summary>
		/// <returns></returns>
		public override AbstractTask Instantiate ()
		{
			var instantiatedTask = CreateInstance<WithdrawTask> ();
			instantiatedTask.duration = this.duration;
			instantiatedTask.amount = this.amount;
			return instantiatedTask;
		}

		/// <summary>
		/// This Task places the storage balance into the workers temporary storage.
		/// </summary>
		/// <param name="controller"></param>
		/// <param name="onComplete"></param>
		public override void DoTask (WorkerController controller, IStorage storage, Action onComplete)
		{
			//Debug.Log ("Task: Withdraw for " + duration + " seconds");
			controller.Agent.StartCoroutine (Withdrawing (controller, storage, onComplete));
		}

		/// <summary>
		/// Depositings the specified controller.
		/// </summary>
		/// <param name="controller">The controller.</param>
		/// <param name="storage">The storage.</param>
		/// <returns></returns>
		private IEnumerator Withdrawing (WorkerController controller, IStorage storage, Action onComplete)
		{
			yield return new WaitForSeconds (duration);

			if (storage.Withdraw (amount))
			{				
				controller.TemporaryStorage.Deposit (amount);
			}
			else
			{
				// If the storage doesnt have the amount, worker takes whatever is in the storage
				var amountLeftOver = storage.GetBalance ();
				storage.Withdraw (amountLeftOver);
				controller.TemporaryStorage.Deposit (amountLeftOver);
			}

			onComplete ();
		}

		#endregion

	}
}
