using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BeliefEngine.HealthKit
{

/*! @brief Wraps information about an error from HealthKit.
	Mainly just a wrapper around Objective-C NSError objects
 */
public class Error :System.Object {
	
	public int code;					 	/*!< @brief The error code. */
	public string domain;					/*!< @brief The error domain. */
	public string localizedDescription; 	/*!< @brief The localized (if available) description of the error. */
	
	/*!	@brief The designated constructor.
		@param node an XML node containing the serialization of the error.
	 */
	public Error(XmlNode node) {
		this.code = Int32.Parse(node["code"].InnerText);
		this.domain = node["domain"].InnerText;
		XmlNode userInfo = node["userInfo"];
		this.localizedDescription = userInfo["NSLocalizedDescription"].InnerText;
	}
}

}