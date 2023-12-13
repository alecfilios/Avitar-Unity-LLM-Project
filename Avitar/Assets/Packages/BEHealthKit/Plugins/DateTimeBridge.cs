using UnityEngine;
using System;
using System.Globalization;

namespace BeliefEngine.HealthKit
{

/*! @brief A small helper class to bridge dates between C# and Objective-C
 */
public class DateTimeBridge : ScriptableObject {
	//										 2017-04-25T14:18:52-07:00
	// 										 2017-04-25T22:12:50Z
	//										 2017-04-26T01:14:43
	public static string dateBridgeFormat = "yyyy-MM-ddTHH:mm:ss"; /*!< @brief all timestamps from Objective-C should be in this format." */
	
	private static DateTimeOffset referenceDate {
		get { return new DateTimeOffset(1970, 1, 1, 0, 0, 0, 0, TimeSpan.Zero); }
	}

	/*! @brief 		Convert a DateTimeOffset to a string datestamp to be passed to Objective-C
		@param date	the date to convert. 
	 */
	public static string DateToString(DateTimeOffset date) {
		TimeSpan span = date - DateTimeBridge.referenceDate;
		double interval = span.TotalSeconds;
		string dateString = Convert.ToString(interval, new CultureInfo("en-US"));
		// Debug.Log("date string:" + dateString);
		return dateString;
	}
	
	/*! @brief 			Convert a string timestamp from Objective-C to a DateTimeOffset 
	 	@param stamp	the timestamp to convert.
	 */
	public static DateTimeOffset DateFromString(string stamp) {
		Double d;
		DateTimeOffset ret;
		if (Double.TryParse(stamp, NumberStyles.AllowDecimalPoint, CultureInfo.GetCultureInfo("en-US"), out d)) {
			DateTimeOffset date = DateTimeBridge.referenceDate.AddSeconds(d);
			// return TimeZoneInfo.ConvertTime(date, TimeZoneInfo.Local);
			ret = date.ToLocalTime();
		} else {
			Debug.LogErrorFormat("error parsing '{0}'", stamp);
			ret = new DateTimeOffset();
		}
		// Debug.LogFormat("stamp: {0} => date: {1}", stamp, ret);
		return ret;
	}
} 

}