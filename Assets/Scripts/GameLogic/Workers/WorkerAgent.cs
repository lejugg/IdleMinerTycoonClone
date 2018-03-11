using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

namespace IdleMinerTycoonClone.GameLogic.Workers
{

	/// <summary>
	/// A WorkerAgent represents scene Monobehaviour of a Worker.
	/// </summary>
	public class WorkerAgent : MonoBehaviour
	{
		#region serialized fields

		/// <summary>
		/// The movement speed in units per second.
		/// </summary>
		[SerializeField]
		private float movementSpeed = 1f;

		/// <summary>
		/// The body collider.
		/// </summary>
		[SerializeField]
		private Button bodyCollider;

		/// <summary>
		/// The display text.
		/// </summary>
		[SerializeField]
		private Text displayText;

		#endregion

		#region private fields

		/// <summary>
		/// The current movement target.
		/// </summary>
		private Vector2 currentTargetPosition;

		/// <summary>
		/// The on movement complete callback
		/// </summary>
		private Action onMovementCompleteCallback;

		#endregion

		#region events

		public delegate void ClickAction ();

		/// <summary>
		/// The on agent clicked event.
		/// </summary>
		public ClickAction OnAgentClicked;

		#endregion

		#region properties

		/// <summary>
		/// Gets or sets a value indicating whether this instance is moving.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is moving; otherwise, <c>false</c>.
		/// </value>
		public bool IsMoving
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the movement speed.
		/// </summary>
		/// <value>
		/// The movement speed.
		/// </value>
		public float MovementSpeed
		{
			get
			{
				return movementSpeed * Time.deltaTime;
			}
		}

		#endregion

		#region unity functions

		/// <summary>
		/// Awakes this instance.
		/// </summary>
		private void Awake ()
		{
			bodyCollider.onClick.AddListener (HandleAgentClicked);			
		}

		#endregion

		#region click handling

		/// <summary>
		/// Called when [agent clicked].
		/// </summary>
		private void HandleAgentClicked ()
		{
			OnAgentClicked ();
		}

		#endregion

		#region movement logic

		/// <summary>
		/// Moves to.
		/// </summary>
		/// <param name="position">The position.</param>
		public void MoveTo (Vector2 position, Action onMovementCompleteCallback)
		{
			IsMoving = true;
			currentTargetPosition = position;
			this.onMovementCompleteCallback = onMovementCompleteCallback;
		}

		/// <summary>
		/// Move Agent in Late update.
		/// </summary>
		private void LateUpdate ()
		{
			if (IsMoving)
			{
				MoveToTarget ();
			}
			
		}

		/// <summary>
		/// Moves to target.
		/// </summary>
		private void MoveToTarget ()
		{
			if (Vector3.Distance (this.transform.position, currentTargetPosition) > MovementSpeed)
			{
				var position2D = new Vector2 (this.transform.position.x, this.transform.position.y);
				var velocity = (currentTargetPosition - position2D).normalized * MovementSpeed;
				this.transform.position = position2D + velocity;
			}
			else
			{
				this.transform.position = currentTargetPosition;
				currentTargetPosition = Vector3.zero;

				if (onMovementCompleteCallback != null)
				{
					onMovementCompleteCallback ();
				}
				onMovementCompleteCallback = null;

				IsMoving = false;
			}
		}

		#endregion

		#region utility functions

		/// <summary>
		/// Sets the display text.
		/// </summary>
		/// <param name="text">The text.</param>
		public void SetDisplayText (string text)
		{
			if (displayText != null)
			{
				this.displayText.text = text;
			}			
		}

		#endregion
	}

}
