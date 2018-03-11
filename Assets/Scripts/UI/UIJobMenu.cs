using IdleMinerTycoonClone.GameLogic.Jobs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace IdleMinerTycoonClone.UI
{
	/// <summary>
	/// A job Menu is the permanent UI feature, that allows interaction with a job monobehaviour.
	/// </summary>
	/// <seealso cref="IdleMinerTycoonClone.UI.UIAbstractMenu" />
	public class UIJobMenu : UIAbstractMenu
	{
		#region serialized fields

		/// <summary>
		/// The referenced job.
		/// </summary>
		[SerializeField]
		private Job job;

		/// <summary>
		/// The start button
		/// </summary>
		[SerializeField]
		private Button startButton;

		/// <summary>
		/// The manager button.
		/// </summary>
		[SerializeField]
		private Button managerButton;
		
		#endregion

		#region interaction

		/// <summary>
		/// Awakes this instance.
		/// </summary>
		private void Awake ()
		{
			if (startButton != null)
			{
				startButton.onClick.AddListener (HandleStartButtonClicked);
			}

			if (managerButton != null)
			{
				managerButton.onClick.AddListener (HandleManagerButtonClicked);
			}
		}

		private void HandleManagerButtonClicked ()
		{
			if (job != null)
			{
				Debug.Log ("Start Working");
				job.StartWorking ();
				managerButton.GetComponent<Image> ().color = new Color (0.1f, 0.5f, 0.1f);
			}
		}

		/// <summary>
		/// Handles the start button clicked.
		/// </summary>
		private void HandleStartButtonClicked ()
		{
			if (job != null)
			{
				Debug.Log ("Start Job");
				job.StartJob ();
			}
		}

		#endregion
	}
}