using System;
using System.Collections.Generic;
using System.Text;

namespace WargamingAPI.WoT.Exceptions
{
	public class CauseException
	{
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
