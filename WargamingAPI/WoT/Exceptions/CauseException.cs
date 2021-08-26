using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Exceptions
{
	public class CauseException
	{
		/// <summary>
		/// Cause exception, if getting data failed
		/// </summary>
		/// <param name="error">Type error</param>
		/// <exception cref="SearchException">Throws when find a message</exception>
		/// <exception cref="Exception">If message wasn't in the list</exception>
		public void Cause(string error)
		{
			XMLErrors init = new();
			init.ReadErrors();
			List<Error> listErrors = init.Errors;

			for (int i = 0; i < listErrors.Capacity; i++)
			{
				if (string.Equals(error, listErrors[i].TypeError))
				{
					throw new SearchException(listErrors[i].Description);
				}
			}

			throw new Exception("Something went wrong");
		}
	}
}
