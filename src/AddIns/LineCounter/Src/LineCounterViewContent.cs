/*
 * Created by SharpDevelop.
 * User: Daniel Grunwald
 * Date: 09.05.2006
 * Time: 17:53
 */
 
using System;
using System.Windows.Forms;
//using System.Windows.Forms;
using ICSharpCode.SharpDevelop.Gui;
using ICSharpCode.SharpDevelop.Workbench;

namespace LineCounterAddin
{
	/// <summary>
	/// Description of the view content
	/// </summary>
	public class LineCounterViewContent : AbstractViewContent
	{
		LineCounterBrowser browser = new LineCounterBrowser();
		
		/// <summary>
		/// The <see cref="System.Windows.Forms.Control"/> representing the view.
		/// </summary>
		public override object Control {
			get {
				return browser;
			}
		}
		
		public LineCounterViewContent()
		{
			this.TitleName = "Line Counter";
		}
	}
}
