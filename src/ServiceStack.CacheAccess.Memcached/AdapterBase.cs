using System;
using ServiceStack.Logging;

//copied from: namespace ServiceStack.Common.Support
namespace ServiceStack.CacheAccess.Memcached
{
	/// <summary>
	/// Common functionality when creating adapters
	/// </summary>
	public abstract class AdapterBase
	{
		protected abstract ILog Log { get; }

		/// <summary>
		/// Executes the specified expression. 
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="action">The action.</param>
		/// <returns></returns>
		protected T Execute<T>(Func<T> action)
		{
			DateTime before = DateTime.Now;
			Log.DebugFormat("Executing action '{0}'", action.Method.Name);
			try
			{
				T result = action();
				TimeSpan timeTaken = DateTime.Now - before;
				Log.DebugFormat("Action '{0}' executed. Took {1} ms.", action.Method.Name, timeTaken.TotalMilliseconds);
				return result;
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", action.Method.Name, ex.Message);
				throw;
			}
		}

		/// <summary>
		/// Executes the specified action (for void methods).
		/// </summary>
		/// <param name="action">The action.</param>
		protected void Execute(Action action)
		{
			DateTime before = DateTime.Now;
			Log.DebugFormat("Executing action '{0}'", action.Method.Name);
			try
			{
				action();
				TimeSpan timeTaken = DateTime.Now - before;
				Log.DebugFormat("Action '{0}' executed. Took {1} ms.", action.Method.Name, timeTaken.TotalMilliseconds);
			}
			catch (Exception ex)
			{
				Log.ErrorFormat("There was an error executing Action '{0}'. Message: {1}", action.Method.Name, ex.Message);
				throw;
			}
		}
	}
}