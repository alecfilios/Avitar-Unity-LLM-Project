using UnityEngine;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BeliefEngine.HealthKit
{

	/*! @brief Wrapper around HKClinicalRecord.
	 */
	public class ClinicalRecord : Sample
	{
		public string clinicalType;         /*!< an identifier that indicates the type of record, such as an allergic reaction, a lab result, or a medical procedure. */
		public string displayName;          /*!< the display name of this record shown in the Health app */
		public FHIRResource FHIRResource;  /*!< the FHIR data for this record */

		/*! @brief      The default constructor.
			@param node the XmlNode to create this object from.
		 */
		public ClinicalRecord(XmlNode node) : base(node) {
			this.displayName = node["displayName"].InnerText;
			this.FHIRResource = new FHIRResource(node["FHIRResource"]);
		}
	}

	public partial class HealthData : System.Object
	{

		/*! @brief parse XML containing a list of clinical records. */
		public List<ClinicalRecord> ParseClinicalRecords() {
			XmlNodeList sampleNodes = this.xml.SelectNodes("/records/records");
			List<ClinicalRecord> records = new List<ClinicalRecord>();
			foreach (XmlNode node in sampleNodes) {
				records.Add(new ClinicalRecord(node));
			}

			return records;
		}
	}
}