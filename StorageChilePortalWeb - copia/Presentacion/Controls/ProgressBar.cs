using System;
using System.Web.UI;
using System.Collections.Generic;

namespace Presentacion
{
	public class ProgressBar : ScriptControl
	{
		public int PollInterval { get; set; }

		public string ProcessName { get; set; }

		protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			ScriptControlDescriptor descriptor = new ScriptControlDescriptor("Presentacion.ProgressBar", this.ClientID);

			descriptor.AddProperty("pollInterval", this.PollInterval);

			descriptor.AddProperty("processName", this.ProcessName);

			yield return descriptor;
		}

		protected override IEnumerable<ScriptReference> GetScriptReferences()
		{
			yield return new ScriptReference(this.ResolveClientUrl("ProgressBar.js"));
		}
	}
}
