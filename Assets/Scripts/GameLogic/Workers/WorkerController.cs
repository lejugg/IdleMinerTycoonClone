using IdleMinerTycoonClone.GameLogic.Jobs;
using IdleMinerTycoonClone.GameLogic.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IdleMinerTycoonClone.GameLogic.Workers
{

	/// <summary>
	/// A WorkerController represents the AI part of a worker.
	/// </summary>
	public class WorkerController 
	{
		#region private fields

		/// <summary>
		/// The workers tasks.
		/// </summary>
		private Queue<AbstractTask> tasks = new Queue<AbstractTask> ();

		/// <summary>
		/// This jobs given storages.
		/// </summary>
		private List<IStorage> storages;

		/// <summary>
		/// The active task
		/// </summary>
		private AbstractTask activeTask;

		/// <summary>
		/// The agent.
		/// </summary>
		private WorkerAgent agent;

		/// <summary>
		/// The next storage index 
		/// </summary>
		private int nextStorageIndex = 0;

		/// <summary>
		/// The agent is spawned at that storage.
		/// </summary>
		private int startAtStorageIndex = 0;

		/// <summary>
		/// The task coroutine.
		/// </summary>
		private Coroutine taskCoroutine;

		/// <summary>
		/// The temporary storage.
		/// </summary>
		private WorkerStorage temporaryStorage;

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets a value indicating whether this instance is working.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is working; otherwise, <c>false</c>.
		/// </value>
		public bool KeepWorking
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the storages to work with in the tasks.
		/// </summary>
		/// <value>
		/// The storages.
		/// </value>
		public List<IStorage> Storages
		{
			get
			{
				return this.storages;
			}
		}

		/// <summary>
		/// Gets the agent.
		/// </summary>
		/// <value>
		/// The agent.
		/// </value>
		public WorkerAgent Agent
		{
			get
			{
				return this.agent;
			}
		}

		/// <summary>
		/// Gets the temporary storage.
		/// </summary>
		/// <value>
		/// The temporary storage.
		/// </value>
		public WorkerStorage TemporaryStorage
		{
			get
			{
				return temporaryStorage;
			}
		}

		#endregion

		#region initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkerController"/> class.
		/// </summary>
		public WorkerController (List<IStorage> storages, Queue<AbstractTask> tasks, WorkerAgent workerAgent, int startAtStorageIndex)
		{
			this.storages = storages;
			this.tasks = tasks;
			this.startAtStorageIndex = startAtStorageIndex;
			
			SpawnWorkerAgent (workerAgent);

			this.temporaryStorage = new WorkerStorage (Agent);
			
			// work once on start
			StartWorkCoroutine ();
		}

		/// <summary>
		/// Spawns the worker agent.
		/// </summary>
		/// <param name="workerAgent">The worker agent.</param>
		private void SpawnWorkerAgent (WorkerAgent workerAgent)
		{
			this.agent = GameObject.Instantiate<WorkerAgent> (workerAgent, position: storages [startAtStorageIndex].GetPosition (), rotation: Quaternion.identity, parent: WorkerContainer.Instance.transform);
			agent.OnAgentClicked += StartWorkCoroutine;
		}

		/// <summary>
		/// Handles the agent clicked.
		/// </summary>
		public void StartWorkCoroutine ()
		{		
			if(taskCoroutine == null)
			{
				taskCoroutine = Agent.StartCoroutine (Work ());
			}			
		}
		
		#endregion

		#region main logic

		/// <summary>
		/// Performs all tasks once.
		/// </summary>
		/// <returns></returns>
		private IEnumerator PerformAllTasksOnce ()
		{
			Agent.GetComponentInChildren<UnityEngine.UI.Image> ().color = new Color (0.8f ,0.8f, 0.8f);
			var index = 0;

			while (index < tasks.Count)
			{
				yield return PerformNextTask ();
				index++;
			}

			Agent.GetComponentInChildren<UnityEngine.UI.Image> ().color = new Color (1f,1f,1f);
			taskCoroutine = null;
			//Debug.Log ("All Tasks done");
		}

		/// <summary>
		/// Perrforms the next task.
		/// </summary>
		private IEnumerator PerformNextTask ()
		{
			activeTask = tasks.Dequeue ();
			var performingTask = true;
			
			activeTask.DoTask (this, storages[nextStorageIndex], () =>
			{
				tasks.Enqueue (activeTask);
				performingTask = false;
				activeTask = null;
				nextStorageIndex = (nextStorageIndex + 1) % storages.Count;				
			});

			while (performingTask)
			{
				yield return new WaitForEndOfFrame ();
			}
		}

		/// <summary>
		/// Does its work continuously.
		/// </summary>
		public IEnumerator Work ()
		{
			do
			{
				yield return PerformAllTasksOnce ();
			}
			while (KeepWorking);
			
			taskCoroutine = null;
		}

		#endregion
	}

}
