using IdleMinerTycoonClone.GameLogic.Workers;
using UnityEngine;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{

	/// <summary>
	/// A Workerstorage holds his balance while transporting currency.
	/// </summary>
	public class WorkerStorage : IStorage
	{
		#region private fields

		/// <summary>
		/// The balance.
		/// </summary>
		private int balance;

		private WorkerAgent agent;

		#endregion

		#region MyRegion

		/// <summary>
		/// Gets the balance.
		/// </summary>
		/// <value>
		/// The balance.
		/// </value>
		public int Balance
		{
			get
			{
				return this.balance;
			}
		}

		#endregion

		#region initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="WorkerStorage"/> class.
		/// </summary>
		public WorkerStorage (WorkerAgent agent)
		{
			this.agent = agent;
			agent.SetDisplayText (Balance.ToString());
		}

		#endregion

		#region implement IStorage

		/// <summary>
		/// Adds the currency. This storage can only be filled until it is full.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public bool Deposit (int amount, bool repeatForEachLinkedStorage = true)
		{			
			this.balance += amount;
			agent.SetDisplayText (Balance.ToString ());
			return true;
		}

		/// <summary>
		/// Determines whether the specified amount is sufficient.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>
		///   <c>true</c> if the specified amount is sufficient; otherwise, <c>false</c>.
		/// </returns>
		public bool IsSufficient (int amount)
		{
			return this.balance >= amount;
		}

		/// <summary>
		/// Gets the balance.
		/// </summary>
		/// <returns></returns>
		public int GetBalance ()
		{
			return Balance;
		}

		/// <summary>
		/// Withdraws from the storage.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public bool Withdraw (int amount, bool repeatForEachLinkedStorage = true)
		{
			if (IsSufficient (amount))
			{
				this.balance -= amount;
				agent.SetDisplayText (Balance.ToString ());
				return true;
			}

			return false;
		}

		/// <summary>
		/// Sets the balance.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public void SetBalance (int amount)
		{
			this.balance = amount;
		}

		#endregion

		#region utility functions

		/// <summary>
		/// Withdraws all.
		/// </summary>
		/// <returns></returns>
		public int WithdrawAll ()
		{
			var tempBalance = this.balance;			
			this.balance = 0;
			agent.SetDisplayText (Balance.ToString ());
			return tempBalance;
		}

		/// <summary>
		/// Return no position.
		/// </summary>
		/// <returns></returns>
		public Vector2 GetPosition ()
		{
			return Vector3.zero;
		}

		#endregion
	}

}
