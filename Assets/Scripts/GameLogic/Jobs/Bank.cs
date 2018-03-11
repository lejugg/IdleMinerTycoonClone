using IdleMinerTycoonClone.General;
using UnityEngine;
using UnityEngine.UI;

namespace IdleMinerTycoonClone.GameLogic.Jobs
{

	/// <summary>
	/// The Bank holds the global currency. This can be extended with a Dictionary instead
	/// of an int field, to organize different currencies.
	/// </summary>
	/// <seealso cref="IdleMinerTycoonClone.General.AbstractSingletonMonoBehaviour" />
	/// <seealso cref="IdleMinerTycoonClone.GameLogic.AbstractSingletonMonoBehaviour" />
	public class Bank : Singleton<Bank>, IStorage
	{
		#region serialized fields

		/// <summary>
		/// The account display
		/// </summary>
		[SerializeField]
		private Text accountDisplay;

		#endregion
		
		#region private fields

		/// <summary>
		/// The balance.
		/// </summary>
		private int balance;

		/// <summary>
		/// The player prefs identifier.
		/// </summary>
		private const string playerPrefsId = "Bank_Balance";

		#endregion

		#region properties

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
			private set
			{
				this.balance = value;
				PlayerPrefs.SetInt (playerPrefsId, this.balance);
			}			
		}

		#endregion

		#region account logic

		/// <summary>
		/// Adds the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public bool Deposit (int amount, bool repeatForEachLinkedStorage = true)
		{
			this.Balance += amount;
			SetAccountDisplayText ();
			return true;
		}

		/// <summary>
		/// Spends the currency.
		/// </summary>
		/// <param name="amount">The amount.</param>
		/// <returns></returns>
		public bool Withdraw (int amount, bool repeatForEachLinkedStorage = true)
		{
			if (IsSufficient (amount))
			{
				this.Balance -= amount;
				SetAccountDisplayText ();
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
		public bool IsSufficient (int amount)
		{
			return this.Balance >= amount;
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
		/// Sets the balance.
		/// </summary>
		/// <param name="amount">The amount.</param>
		public void SetBalance (int amount)
		{
			this.balance = amount;
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

		#region unity functions

		/// <summary>
		/// Sets the balance on awake.
		/// </summary>
		private void Awake ()
		{
			this.balance = PlayerPrefs.GetInt (playerPrefsId);
			SetAccountDisplayText ();
		}

		#endregion

		#region utility functions

		/// <summary>
		/// Sets the account display text.
		/// </summary>
		private void SetAccountDisplayText ()
		{
			if (accountDisplay != null)
			{
				accountDisplay.text = Balance.ToString () + " $";
			}

		}

		#endregion
	}

}
