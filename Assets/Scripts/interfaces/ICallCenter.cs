
	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary>
	///   The Call Center Interface.  Represents A System That Manages Incoming Calls And Appropriately
	///   Handles Them By Dispatching Them To The Correct Employee.  Calls Are Handled In A Multi-
	///   Threaded Enviroment Where Each Call Gets Dispatched In Its Own Thread.
	/// </summary>
	/// <remarks> Manan Parikh </remarks>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public interface ICallCenter
	{

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///   Updates This Instance.  Used To Perform Updated On Any Calls In The Wait Queue.  Must Be
    ///   Called Every Frame.
    /// </summary>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
		void Update();

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///   Handles The Specified Call In A Separate Thread By Intellegently Querying Employees That Are
    ///   Available To Handle It Based On Availability And Skill.
    /// </summary>
    /// <param name="Call"> The Call You Want To Handle. </param>
    /// <param name="bRequestedEscalation">
    ///   Specifies Whether Or Not The Call Center Is Being Asked To Handle An Existing Call By
    ///   Escalating Up The Chain Of Command Or A New Call.
    /// </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
		void HandleCall( ICall Call, bool bRequestedEscalation = false );

    ////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary> Adds An Employee To The Work Force. </summary>
    /// <param name="Employee"> The Employee To Add. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
		void AddEmployee( IEmployee Employee );
	}