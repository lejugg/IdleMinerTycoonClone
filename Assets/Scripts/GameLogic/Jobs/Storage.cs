using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{

	/// <summary>
	/// A storage is in the scene and can be accessed by workers.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	/// <seealso cref="IdleMinerTycoonClone.GameLogic.Jobs.IStorage" />
	[AddComponentMenu ("Jobs/Storage")]
	public class Storage : MonoBehaviour, IStorage
	{
		#region serialized fields

		/// <summary>
		/// The linked storages
		/// </summary>
		[SerializeField, Tooltip("These storages will share the exact same data")]
		private List<Storage> linkedStorages = new List<Storage> ();

		/// <summary>
		/// The display
		/// </summary>
		[SerializeField]
		private Text display;

		/// <summary>
		/// This storage has infinite resources.
		/// </summary>
		[SerializeField, Header("Set isInfinite, or set start values")]
		private bool isInfiniteResource = false;

		/// <summary>
		/// The start balance.
		/// </summary>
		[SerializeField]
		private int startBalance = 1000;

		/// <summary>
		/// This storage has an infinite storage size.
		/// </summary>
		[SerializeField]
		private bool isInfiniteStorageSize = false;

		/// <summary>
		/// The storage maximum size.
		/// </summary>
		[SerializeField]
		private int storageMaxSize = 100000;

		#endregion

		#region private fields

		/// <summary>
		/// The balance.
		/// </summary>
		private int balance;

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

		/// <summary>
		/// Gets or sets the maximum size of the storage.
		/// </summary>
		/// <value>
		/// The maximum size of the storage.
		/// </value>
		public int StorageMaxSize
		{
			get
			{
				return this.storageMaxSize;
			}
			set
			{
				this.storageMaxSize = value;
			}
		}

		#endregion

		#region unity functions

		/// <summary>
		/// Called when [enable].
		/// </summary>
		private void OnEnable ()
		{
			this.balance = startBalance;

			ForEachLinkedStorage ( (storage) => storage.SetBalance(this.balance));

			SetDisplayText ();
		}

		#endregion

		#region implement IStorage

		/// <summary>
		/// Adds the currency. This storage can only be filled until it is full.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public virtual bool Deposit (int amount, bool repeatForEachLinkedStorage = true)
		{
			if (!IsStorageFull (amount))
			{
				this.balance += amount;

				if (repeatForEachLinkedStorage)
				{
					ForEachLinkedStorage ((storage) => storage.Deposit (amount, false));
				}

				SetDisplayText ();
				
				return true;
			}
			return false;
		}

		/// <summary>
		/// Determines whether the specified amount is sufficient.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>
		///   <c>true</c> if the specified amount is sufficient; otherwise, <c>false</c>.
		/// </returns>
		public virtual bool IsSufficient (int amount)
		{
			return isInfiniteResource || this.balance >= amount;
		}

		/// <summary>
		/// Withdraws from the storage.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public virtual bool Withdraw (int amount, bool repeatForEachLinkedStorage = true)
		{
			if (IsSufficient (amount))
			{
				this.balance -= amount;

				if (repeatForEachLinkedStorage)
				{
					ForEachLinkedStorage ((storage) => storage.Withdraw (amount, false));
				}

				SetDisplayText ();

				return true;
			}

			return false;
		}

		/// <summary>
		/// Gets the balance.
		/// </summary>
		/// <returns></returns>
		public virtual int GetBalance ()
		{
			return Balance;
		}


		/// <summary>
		/// Sets the balance.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public virtual void SetBalance (int amount)
		{
			this.balance = amount;
		}

		#endregion

		#region utility functions

		/// <summary>
		/// Sets the display text.
		/// </summary>
		private void SetDisplayText ()
		{
			if (display != null)
			{
				display.text = this.Balance.ToString ();
			}			
		}

		/// <summary>
		/// Fors the each.
		/// </summary>
		/// <param name="job">The job.</param>
		public void ForEachLinkedStorage (Action<Storage> job)
		{
			for (int i = 0; i < linkedStorages.Count; i++)
			{
				job (linkedStorages [i]);
			}
		}

		/// <summary>
		/// Determines whether [is storage full] [the specified amount].
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>
		///   <c>true</c> if [is storage full] [the specified amount]; otherwise, <c>false</c>.
		/// </returns>
		public bool IsStorageFull (int amount)
		{
			return !isInfiniteStorageSize && (this.balance + amount) > storageMaxSize;
		}

		/// <summary>
		/// Withdraws all.
		/// </summary>
		/// <returns></returns>
		public int WithdrawAll ()
		{
			var tempBalance = this.balance;
			this.balance = 0;
			return tempBalance;
		}

		/// <summary>
		/// Gets the position in the scene.
		/// </summary>
		/// <returns></returns>
		public Vector2 GetPosition ()
		{
			return this.transform.position;
		}

		#endregion
	}

}
