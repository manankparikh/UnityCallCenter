using System;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	///   The Problem ID Enum.  Used To Define The Available Types Of Problems A Caller Can Have.
	///   Certain Problems Require Special Employees.
	/// </summary>
	/// <remarks> Manan Parikh </remarks>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	[Flags]
	public enum EProblemID
	{
		/// <summary> The Level1 Entry.  Represents A Level 1 Problem. </summary>
		Level1 = 1 << 0,

		/// <summary> The Level2 Entry.  Represents A Level 2 Problem. </summary>
		Level2 = 1 << 1,

		/// <summary> The Level3 Entry.  Represents A Level 3 Problem. </summary>
		Level3 = 1 << 2,

		/// <summary> The Level4 Entry.  Represents A Level 4 Problem. </summary>
		Level4 = 1 << 3,

		/// <summary> The Level5 Entry.  Represents A Level 5 Problem. </summary>
		Level5 = 1 << 4,

		/// <summary> The Level6 Entry.  Represents A Level 6 Problem. </summary>
		Level6 = 1 << 5,

		/// <summary> The Level7 Entry.  Represents A Level 7 Problem. </summary>
		Level7 = 1 << 6,

		/// <summary> The Level8 Entry.  Represents A Level 8 Problem. </summary>
		Level8 = 1 << 7,

		/// <summary> The Level9 Entry.  Represents A Level 9 Problem. </summary>
		Level9 = 1 << 8,

		/// <summary> The Level10 Entry.  Represents A Level 10 Problem. </summary>
		Level10 = 1 << 9,

		/// <summary> The Level11 Entry.  Represents A Level 11 Problem. </summary>
		Level11 = 1 << 10,

		/// <summary> The Level12 Entry.  Represents A Level 12 Problem. </summary>
		Level12 = 1 << 11,
	}

	public interface ICall
	{
		void AssignCall( IEmployee Employee );
		IEmployee GetCurrentEmployee();

		
		void SetID( uint ID );

		
		uint GetID();
		void SetProblemID( EProblemID problemID );

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///   Gets This Instance's ProblemID.  That Is, The ID Of The Problem To Be Solved.
		/// </summary>
		/// <returns> This Instance's ProblemID. </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		EProblemID GetProblemID();

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> Gets This Instance's Completion Time. </summary>
		/// <returns> This Instance's Completion Time. </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		float GetCompletionTime();

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> Gets Whether This Instance Has Been Fully Handled. </summary>
		/// <returns> Whether This Instance Has Been Fully Handled. </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		bool WasHandled();
	}
