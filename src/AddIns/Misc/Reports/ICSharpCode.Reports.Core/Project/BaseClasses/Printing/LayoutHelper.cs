﻿/*
 * Created by SharpDevelop.
 * User: Peter Forstmeier
 * Date: 17.02.2011
 * Time: 20:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using ICSharpCode.Reports.Core.Globals;
using ICSharpCode.Reports.Core.Interfaces;

namespace ICSharpCode.Reports.Core.BaseClasses.Printing
{
	
	internal sealed class LayoutHelper
	{
		public static Rectangle FixSectionLayout(Graphics graphics,BaseSection section)
		{
			ILayouter layouter = (ILayouter)ServiceContainer.GetService(typeof(ILayouter));
			var desiredRectangle = layouter.Layout(graphics, section);
			return desiredRectangle;
		}
		
		
		public static void SetLayoutForRow (Graphics graphics, ILayouter layouter,ISimpleContainer row)
		{
			Rectangle textRect = layouter.Layout(graphics,row);
			if (textRect.Height > row.Size.Height) {
				row.Size = new Size(row.Size.Width,textRect.Height + 5);
			}
		}
		
	}
}
