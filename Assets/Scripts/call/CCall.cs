using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class CCall : ICall
{
	private bool _handled = false;
	private IEmployee _currentEmployee = null;
	private uint _id = 0;
	private EProblemID _problemID;
	private float _completionTime;
	private object _lock = new object();
	private static object _staticLock = new object();

	private static System.Random _random = new System.Random();

	////////////////////////////////////////////////////////////////////////////////////////////////////
    /// <summary> Assign the call to the employee </summary>
    /// <param name="Employee"> employee. </param>
    ////////////////////////////////////////////////////////////////////////////////////////////////////
	public void AssignCall( IEmployee Employee )
	{
		lock ( _staticLock )
		{
			_completionTime = _random.Next( 0, 300 );
		}

		lock ( _lock )
		{
			Employee.SetBusy( true );
			_currentEmployee = Employee;
			if ( ( _currentEmployee.GetSupportedProblemIDs() & _problemID ) == _problemID )
			{
				_handled = true;
				GUI.Instance.AppendLog( String.Format( "Call {0} With ProblemID {1} Was Handled By Employee {2} With Designation {3}.  Call Took {4} Seconds", GetID(), GetProblemID(), _currentEmployee.GetID(), _currentEmployee.GetDesignation(), _completionTime ) );
			}
			else
			{
				GUI.Instance.AppendLog( String.Format( "Call {0} With ProblemID {1} Was Escalated To {2}", GetID(), GetProblemID(), ( EEmployeeDesignation ) ( ( uint ) _currentEmployee.GetDesignation() + 1 ) ) );
				
				CCallCenter.Instance().HandleCall( this, true );
			}
			Employee.SetBusy( false );
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary> getter for the employee currently busy with this call </summary>
	/// <returns> employee busy with the call </returns>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public IEmployee GetCurrentEmployee()
	{
		lock ( _lock )
		{
			return _currentEmployee;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary> setter for the callID </summary>
	/// <param name="ID"> the callID </param>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SetID( uint ID )
	{
		lock ( _lock )
		{
			_id = ID;
		}
	}

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary> getter for the callID. </summary>
	/// <returns> the callID </returns>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public uint GetID()
	{
		lock ( _lock )
		{
			return _id;
		}
	}
	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <summary> setter for the problemID </summary>
	/// <param name="problemID"> the problemID </param>
	////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SetProblemID( EProblemID problemID )
	{
		lock ( _lock )
		{
			_problemID = problemID;
		}
	}

	public EProblemID GetProblemID()
	{
		lock ( _lock )
		{
			return _problemID;
		}
	}

	public float GetCompletionTime()
	{
		lock ( _lock )
		{
			return _completionTime;
		}
	}

	public bool WasHandled()
	{
		lock ( _lock )
		{
			return _handled;
		}
	}
}