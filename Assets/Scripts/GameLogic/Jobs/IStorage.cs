using UnityEngine;
using System.Collections;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{

	/// <summary>
	/// A storage is used to temporarily hold currency, and allows to deposit or withdraw.
	/// </summary>
	/// <seealso cref="UnityEngine.MonoBehaviour" />
	public interface IStorage
	{
		#region MyRegion

		/// <summary>
		/// Gets the position in the scene.
		/// </summary>
		/// <returns></returns>
		Vector2 GetPosition ();

		#endregion

		#region account logic

		/// <summary>
		/// Gets the balance.
		/// </summary>
		/// <returns></returns>
		int GetBalance ();

		/// <summary>
		/// Sets the balance.
		/// </summary>
		/// <param name="amount">The amount.</param>

		void SetBalance (int amount);

		/// <summary>
		/// Adds the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		bool Deposit (int amount, bool repeatForEachLinkedStorage = true);

		/// <summary>
		/// Spends the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		bool Withdraw (int amount, bool repeatForEachLinkedStorage = true);

		/// <summary>
		/// Determines whether the specified amount is sufficient.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns>
		///   <c>true</c> if the specified amount is sufficient; otherwise, <c>false</c>.
		/// </returns>
		bool IsSufficient (int amount);
		
		#endregion
	}

}
