using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

	public class CProjectManager : IEmployee
	{
		/// <summary> This Instance's ID.  Used To Uniquely Identify This Instance. </summary>
		private uint _id = 0;

		/// <summary> Specifies Whether This Instance Is Currently Busy Or Not. </summary>
		private bool _busy = false;

		/// <summary> The Object Used To Syncronize Thread Access. </summary>
		private object _lock = new object();

		public EEmployeeDesignation GetDesignation()
		{
			return EEmployeeDesignation.ProjectManager;
		}

		public EProblemID GetSupportedProblemIDs()
		{
			//Project Manager Employees Can Handle Any Caller Issue
			return
				EProblemID.Level1 | EProblemID.Level2 | EProblemID.Level3 | EProblemID.Level4 |
				EProblemID.Level5 |	EProblemID.Level6 | EProblemID.Level7 | EProblemID.Level8 |
				EProblemID.Level9 |	EProblemID.Level10 | EProblemID.Level11 | EProblemID.Level12;
		}

		public void SetID( uint ID )
		{
			//Syncronize Thread Access
			lock ( _lock )
			{
				_id = ID; 
			}
		}

		public uint GetID()
		{
			//Syncronize Thread Access
			lock ( _lock )
			{
				return _id;
			}
		}

		public void SetBusy( bool bBusy )
		{
			//Syncronize Thread Access
			lock ( _lock )
			{
				_busy = bBusy;
			}
		}

		public bool IsBusy()
		{
			//Syncronize Thread Access
			lock ( _lock )
			{
				return _busy;
			}
		}
	}