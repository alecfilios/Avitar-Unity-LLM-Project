using UnityEngine;
using System;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace BeliefEngine.HealthKit
{

	/*! @brief A small helper class to bridge dates between C# and Objective-C
	 */
	public class TimeSpanBridge : ScriptableObject
	{
		/*! @brief      Convert a TimeSpan to a XML string to be passed to Objective-C
			@param span the TimeSpan to convert. 
		 */
		public static string TimeSpanToXML(TimeSpan span) {
			XElement el = new XElement("timespan",
				new XElement("s", span.Seconds),
				new XElement("m", span.Minutes),
				new XElement("H", span.Hours),
				new XElement("d", span.Days)
			);
			return el.ToString();
		}

		/*! @brief 			Convert an XML string from Objective-C to a TimeSpan
			@param xml	    the xml string to convert.
		 */
		public static TimeSpan TimeSpanFromString(string xml) {
			return TimeSpan.Zero;
		}
	}

}