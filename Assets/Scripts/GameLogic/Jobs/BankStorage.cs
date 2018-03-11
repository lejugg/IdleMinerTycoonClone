using UnityEngine;
using System.Collections;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{
	/// <summary>
	/// The storage forwards its transactions to the bank.
	/// </summary>
	/// <seealso cref="IdleMinerTycoonClone.GameLogic.Jobs.Storage" />
	public class BankStorage : Storage
	{
		#region storage overrides

		/// <summary>
		/// Adds the currency. This storage can only be filled until it is full.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="repeatForEachLinkedStorage"></param>
		/// <returns></returns>
		public override bool Deposit (int amount, bool repeatForEachLinkedStorage = true)
		{
			return (Bank.Instance as Bank).Deposit (amount);
		}

		/// <summary>
		/// Withdraws from the storage.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <param name="repeatForEachLinkedStorage"></param>
		/// <returns></returns>
		public override bool Withdraw (int amount, bool repeatForEachLinkedStorage = true)
		{
			return (Bank.Instance as Bank).Withdraw (amount, repeatForEachLinkedStorage);
		}

		/// <summary>
		/// Determines whether the specified amount is sufficient.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>
		///   <c>true</c> if the specified amount is sufficient; otherwise, <c>false</c>.
		/// </returns>
		public override bool IsSufficient (int amount)
		{
			return (Bank.Instance as Bank).IsSufficient (amount);
		}

		/// <summary>
		/// Gets the balance.
		/// </summary>
		/// <returns></returns>
		public override int GetBalance ()
		{
			return (Bank.Instance as Bank).GetBalance ();
		}

		/// <summary>
		/// Sets the balance.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public override void SetBalance (int amount)
		{
			(Bank.Instance as Bank).SetBalance (amount);
		}

		#endregion
	}

}
