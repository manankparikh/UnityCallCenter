using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

	////////////////////////////////////////////////////////////////////////////////////////////////////
	/// <remarks> Manan Parikh </remarks>
	////////////////////////////////////////////////////////////////////////////////////////////////////

	public class CCallCenter : ICallCenter
	{
		private List<IEmployee> _employees = new List<IEmployee>();
		private Dictionary<ICall, EEmployeeDesignation> _queuedCalls = new Dictionary<ICall, EEmployeeDesignation>();
		private static ICallCenter _instance = null;
		private static uint _currentEmployeeIDGenerated = 0;
		private static uint _currentCallIDGenerated = 0;
		private object _lock = new object();
		private static object _staticLock = new object();
		private static System.Random _random = new Random();
		
		private CCallCenter()
		{

		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> getter for the Singleton Instance of this class </summary>
		///
		/// <returns> Singleton Instance of this class. </returns>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		public static ICallCenter Instance()
		{
			//Instantiate Instance If One Hasn't Already Been Created.
			if ( _instance == null )
				_instance = new CCallCenter();
			return _instance;
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> Add the employee </summary>
		///
		/// <param> Employee </param>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		public void AddEmployee( IEmployee Employee )
		{
			lock ( _lock )
			{
				lock ( _staticLock )
				{
					Employee.SetID( _currentEmployeeIDGenerated++ );
				}
				_employees.Add( Employee );
			}
		}

		////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary> Handle and assign the calls in the queue </summary>
		///
		/// <param> Call </param>
		/// <param> bRequestedEscalation a boolean value requesting escalation or not </param>
		////////////////////////////////////////////////////////////////////////////////////////////////////
		public void HandleCall( ICall Call, bool bRequestedEscalation )
		{
			if ( bRequestedEscalation )
			{
				lock ( _lock )
				{
					EEmployeeDesignation Designation = ( EEmployeeDesignation ) ( ( ( uint ) Call.GetCurrentEmployee().GetDesignation() ) + 1 );
					var CompatableEmployees = _employees.Where( ( e ) => e.GetDesignation() == Designation );

					bool bFoundAvailableEmployee = false;
					foreach ( var employee in CompatableEmployees )
					{
						if ( !employee.IsBusy() )
						{
							new Thread( () => Call.AssignCall( employee ) ).Start();
							bFoundAvailableEmployee = true;
							break;
						}
					}
					if ( !bFoundAvailableEmployee )
						if ( !_queuedCalls.ContainsKey( Call ) )
							_queuedCalls.Add( Call, Designation );
				}
			}
			else
			{
				uint CallID = 0;
				int ProblemIDNumber = 0;
				lock ( _staticLock )
				{
					CallID = _currentCallIDGenerated++;
					ProblemIDNumber = ( _random.Next( 0, 6 ) + _random.Next( 0, 6 ) );
					ProblemIDNumber = ProblemIDNumber == 0 ? 0 : ProblemIDNumber - 1;
				}

				lock ( _lock )
				{
					Call.SetID( CallID );
					Call.SetProblemID( ( EProblemID ) ( 1 << ProblemIDNumber ) );
					var FresherEmployees = _employees.Where( ( e ) => e.GetDesignation() == EEmployeeDesignation.Fresher );

					bool bFoundAvailableEmployee = false;
					foreach ( var employee in FresherEmployees )
					{
						if ( !employee.IsBusy() )
						{
							new Thread( () => Call.AssignCall( employee ) ).Start();
							bFoundAvailableEmployee = true;
							break;
						}
					}
					if ( !bFoundAvailableEmployee )
						_queuedCalls.Add( Call, EEmployeeDesignation.Fresher );
				}
			}
		}

		public void Update()
		{
			lock ( _lock )
			{
				List<ICall> DispatchedCalls = new List<ICall>();
				foreach ( var call in _queuedCalls )
				{
					foreach ( var employee in _employees )
					{
						if ( !employee.IsBusy() && employee.GetDesignation() == call.Value && !call.Key.WasHandled() )
						{
							new Thread( () => call.Key.AssignCall( employee ) ).Start();
							DispatchedCalls.Add( call.Key );
							break;
						}
					}
				}
				foreach ( var call in DispatchedCalls )
				{
					_queuedCalls.Remove( call );
				}
			}
		}
	}