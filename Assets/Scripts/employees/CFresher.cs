using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class CFresher : IEmployee
	{
		private uint _id = 0;
		private bool _busy = false;
		private object _lock = new object();

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> Gets employeeID </summary>
		/// <returns> employeeID </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		uint IEmployee.GetID()
		{
			lock ( _lock )
			{
				return _id; 
			}
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///  Gets the Designation of the employee (Fresher/TL/PM)
		/// </summary>
		/// <returns> This Instance's Employee Designation. </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		public EEmployeeDesignation GetDesignation()
		{
			return EEmployeeDesignation.Fresher;
		}

		public EProblemID GetSupportedProblemIDs()
		{
			//Freshers can handle all issues except 1 & 7
			return
				EProblemID.Level2 | EProblemID.Level3 | EProblemID.Level4 | EProblemID.Level5 |
				EProblemID.Level6 | EProblemID.Level8 | EProblemID.Level9 | EProblemID.Level10 |
				EProblemID.Level11 | EProblemID.Level12;
		}

		public void SetBusy( bool bBusy )
		{
			lock ( _lock )
			{
				_busy = bBusy;
			}
		}

		public bool IsBusy()
		{
			lock ( _lock )
			{
				return _busy;
			}
		}
		
		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		///   Sets the employeID
		/// </summary>
		/// <param name="ID"> employeeID </param>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetID( uint ID )
		{
			lock ( _lock )
			{
				_id = ID;
			}
		}
	}