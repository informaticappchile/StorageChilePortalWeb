using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Web.Script.Services;

namespace Presentacion
{
	[ScriptService]
	public class DoStuffWebService : WebService
	{
		private static Random progressRandom = new Random();

		private static Dictionary<string, int> processProgresses = new Dictionary<string, int>();

		[WebMethod, ScriptMethod]
		public void BeginProcess(string processName)
		{
			if (DoStuffWebService.processProgresses.ContainsKey(processName))
			{
				DoStuffWebService.processProgresses.Remove(processName);
			}

			DoStuffWebService.processProgresses.Add(processName, 0);
		}

		[WebMethod, ScriptMethod]
		public int GetProcessProgress(string processName)
		{
			if (DoStuffWebService.processProgresses.ContainsKey(processName))
			{
				DoStuffWebService.processProgresses[processName] += DoStuffWebService.progressRandom.Next(1, 3);

				return Math.Min(100, DoStuffWebService.processProgresses[processName]);
			}
			else
			{
				return 0;
			}
		}

        [WebMethod]
        public int GetStatus()
        {
            int progress = 0; // in percents
                              // your server-side code here
            return progress;
        }
    }
}
