using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using IdleMinerTycoonClone.GameLogic.Workers;
using IdleMinerTycoonClone.GameLogic.Tasks;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{

	/// <summary>
	/// A job is used to define a screenfield in which one or more workers perform tasks.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	public class Job : MonoBehaviour
	{
		#region serialized fields

		/// <summary>
		/// The associated storages to this job.
		/// </summary>
		[SerializeField]
		private List<GameObject> storages;

		/// <summary>
		/// The given tasks to the workers.
		/// </summary>
		[SerializeField]
		private List<AbstractTask> tasks;

		/// <summary>
		/// The worker agent
		/// </summary>
		[SerializeField]
		private WorkerAgent workerAgent;

		/// <summary>
		/// The start task index.
		/// </summary>
		[SerializeField, Tooltip("Defines which task the worker begins with")]
		private int startAtStorageIndex = 0;

		#endregion

		#region private fields

		/// <summary>
		/// The workers.
		/// </summary>
		private List<WorkerController> workers = new List<WorkerController>();

		#endregion

		#region public functions

		/// <summary>
		/// Starts the job.
		/// </summary>
		public void StartJob ()
		{
			var tasksQueue = new Queue<AbstractTask> ();
			for (int i = 0; i < tasks.Count; ++i)
			{				
				tasksQueue.Enqueue (tasks [i].Instantiate ());
			}

			var iStorages = new List<IStorage> ();
			for (int i = 0; i < storages.Count; i++)
			{
				iStorages.Add (storages [i].GetComponent<IStorage> ());
			}
			
			var newWorker = new WorkerController (iStorages, tasksQueue, workerAgent, startAtStorageIndex);

			for (int i = 0; i < workers.Count; i++)
			{
				if (workers [i].KeepWorking)
				{
					newWorker.KeepWorking = true;
				}
			}

			workers.Add (newWorker);
		}

		/// <summary>
		/// Automates the workers to keep working.
		/// </summary>
		public void StartWorking ()
		{
			for (int i = 0; i < workers.Count; i++)
			{
				workers [i].KeepWorking = true;
				workers [i].StartWorkCoroutine ();
			}
		}

		#endregion

	}
}
