using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using BeliefEngine.HealthKit;

namespace BeliefEngine.HealthKit
{

public class HealthKitFullTest : MonoBehaviour {
	
	public Text instructionLabel;
	public Text resultsLabel;
	public Text errorLabel;
	public Dropdown dropdown;
	public Text buttonLabel;

	public HealthKitDataTypes types;

	private HealthStore healthStore;
	private bool reading = false;
	private bool generateDummyData = true;

	void Awake() {
		dropdown.ClearOptions();
		List<string> opts = new List<string>();
		for (int i = 0; i <= (int)HKDataType.HKQuantityTypeIdentifierUVExposure; i++) {
			HKDataType dataType = (HKDataType)i;
			opts.Add(HealthKitDataTypes.GetIdentifier(dataType));
		}

		opts.Add("———");

		for (int i = (int)HKDataType.HKCategoryTypeIdentifierSleepAnalysis; i <= (int)HKDataType.HKCategoryTypeIdentifierVaginalDryness; i++) {
			HKDataType dataType = (HKDataType)i;
			opts.Add(HealthKitDataTypes.GetIdentifier(dataType));
		}

		opts.Add("———");

		for (int i = (int)HKDataType.HKCharacteristicTypeIdentifierBiologicalSex; i <= (int)HKDataType.HKCharacteristicTypeIdentifierWheelchairUse; i++) {
			HKDataType dataType = (HKDataType)i;
			opts.Add(HealthKitDataTypes.GetIdentifier(dataType));
		}

		opts.Add("———");

		for (int i = (int)HKDataType.HKCorrelationTypeIdentifierBloodPressure; i <= (int)HKDataType.HKCorrelationTypeIdentifierFood; i++) {
			HKDataType dataType = (HKDataType)i;
			opts.Add(HealthKitDataTypes.GetIdentifier(dataType));
		}

		opts.Add("———");

		opts.Add(HealthKitDataTypes.GetIdentifier(HKDataType.HKWorkoutTypeIdentifier));

		dropdown.AddOptions(opts);
	}

	void Start() {
		Debug.Log("---------- START ----------");
		this.healthStore = this.GetComponent<HealthStore>();

		if (Application.platform != RuntimePlatform.IPhonePlayer) {
			if (this.instructionLabel != null) {
				this.instructionLabel.fontSize = 20;
				this.instructionLabel.color = Color.red;
				this.instructionLabel.text = "To use this plugin, you MUST run on an iOS device or in the iOS Simulator. \nIt WILL NOT WORK in the editor.";
			}

			string error = "HealthKit only works on iOS devices! It will not work in the Unity Editor.";
			this.errorLabel.text = error;
			Debug.LogError(error);
		} else {
			this.healthStore.Authorize(this.types);
		}
	}

	public void ReadData() {
		Debug.Log("read data...");
		if (!reading) {
			string selectedName = dropdown.options[dropdown.value].text;
			try {
				HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), selectedName);
				reading = true;

				DateTimeOffset now = DateTimeOffset.UtcNow;
				// for this example, we'll read everything from the past 24 hours
				DateTimeOffset start = now.AddDays(-1);

				if (dataType <= HKDataType.HKQuantityTypeIdentifierUVExposure) {
					// quantity-type
					Debug.Log("reading quantity-type...");
					ReadQuantityData(dataType, start, now);
				}
				else if (dataType <= HKDataType.HKCategoryTypeIdentifierMindfulSession) {
					// category-type
					Debug.Log("reading category-type...");
				}
				else if (dataType <= HKDataType.HKCharacteristicTypeIdentifierWheelchairUse) {
					// characteristic-type
					Debug.Log("reading characteristic-type...");
					ReadCharacteristic(dataType);
				}
				else if (dataType <= HKDataType.HKCorrelationTypeIdentifierFood) {
					// correlation-type
					Debug.Log("reading correlation-type...");
					ReadCorrelationData(dataType, start, now);
				}
				else if (dataType == HKDataType.HKWorkoutTypeIdentifier) {
					// finally, workout-type
					Debug.Log("reading workout-type...");
					ReadWorkoutData(dataType, start, now);
				}
				else {
					Debug.LogError(string.Format("data type {0} invalid", HealthKitDataTypes.GetIdentifier(dataType)));
				}
			}
			catch (ArgumentException) {
				// they just selected a divider; nothing to worry about
				Debug.LogFormat("{0} unrecognized", selectedName);
			}
		} else {
			Debug.Log("already reading?");
		}
	}

	public void WriteData() {
		string selectedName = dropdown.options[dropdown.value].text;
		try {
			HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), selectedName);
			reading = true;

			DateTimeOffset now = DateTimeOffset.UtcNow;
			// for this example, we'll say this sample was from the last 10 minutes
			DateTimeOffset start = now.AddMinutes(-10);

			if (dataType <= HKDataType.HKQuantityTypeIdentifierUVExposure) {
				// quantity-type
				Debug.Log("writing quantity-type: using HKQuantityTypeIdentifierDistanceWalkingRunning...");
				Quantity quantity = new Quantity(0.5, "mi");
				this.healthStore.WriteQuantitySample(HKDataType.HKQuantityTypeIdentifierDistanceWalkingRunning, quantity, start, now, delegate (bool success, Error error) {
					if (!success) {
						Debug.LogErrorFormat("error:{0}", error.localizedDescription);
					}
					else {
						Debug.Log(@"success");
					}
				});
			}
			else if (dataType <= HKDataType.HKCategoryTypeIdentifierMindfulSession) {
				// category-type
				Debug.Log("writing category-type: using HKCategoryTypeIdentifierMindfulSession...");
				this.healthStore.WriteCategorySample(HKDataType.HKCategoryTypeIdentifierMindfulSession, 0, start, now, delegate (bool success, Error error) {
					if (!success) {
						Debug.LogErrorFormat("error:{0}", error.localizedDescription);
					}
					else {
						Debug.Log(@"success");
					}
				});
			}
			else if (dataType <= HKDataType.HKCharacteristicTypeIdentifierWheelchairUse) {
				// characteristic-type
				Debug.Log("can't write characteristic-type...");
			}
			else if (dataType <= HKDataType.HKCorrelationTypeIdentifierFood) {
				// correlation-type
				Debug.Log("writing correlation-type is not currently supported...");
			}
			else if (dataType == HKDataType.HKWorkoutTypeIdentifier) {
				// finally, workout-type
				Debug.Log("writing workout-type: using Fitness Gaming workout...");
				Quantity calories = new Quantity(100, "Cal");
				Quantity distance = new Quantity(0.5, "mi");
				this.healthStore.WriteWorkoutSample(WorkoutActivityType.FitnessGaming, start, now, calories, distance, delegate (bool success, Error error) {
					if (!success) {
						Debug.LogErrorFormat("error:{0}", error.localizedDescription);
					}
					else {
						Debug.Log(@"success");
					}
				});
			}
			else {
				Debug.LogError(string.Format("data type {0} invalid", HealthKitDataTypes.GetIdentifier(dataType)));
			}
		}
		catch (ArgumentException) {
			// they just selected a divider; nothing to worry about
			Debug.LogFormat("{0} unrecognized", selectedName);
		}
	}

	// this is an example of reading Category data. You can cast the sample value to whatever appropriate enum for the sample type. See HealthKitDataTypes.cs for other types.
	public void ReadSleep() {
		DateTimeOffset end = DateTimeOffset.UtcNow;
		DateTimeOffset start = end.AddMinutes(-10);

		Debug.Log("reading sleep from " + start + " to " + end);
		this.healthStore.ReadCategorySamples(HKDataType.HKCategoryTypeIdentifierSleepAnalysis, start, end, delegate(List<CategorySample> samples, Error error) {
			string text = "";
			foreach (CategorySample sample in samples) {
				string valueString = ((SleepAnalysis)sample.value == SleepAnalysis.Asleep) ? "Sleeping" : "In Bed";
				string str = string.Format("- {0} from {1} to {2}", valueString, sample.startDate, sample.endDate);
				Debug.Log(str);
				text = text + str + "\n";
			}
			this.resultsLabel.text = text;

			// all done
			reading = false;
		});
	}

	public void ReadFlights() {
		DateTimeOffset end = DateTimeOffset.UtcNow;
		DateTimeOffset start = end.AddMinutes(-10);

		this.resultsLabel.text = string.Format("Reading flights climbed from {0} to {1}...\n", start, end);
		int steps = 0;
		this.healthStore.ReadQuantitySamples(HKDataType.HKQuantityTypeIdentifierFlightsClimbed, start, end, delegate(List<QuantitySample> samples, Error error) {
			Debug.Log("found " + samples.Count + " flights samples");
			foreach (QuantitySample sample in samples) {
				Debug.Log("   - " + sample.quantity.doubleValue + " from " + sample.startDate + " to " + sample.endDate);
				steps += Convert.ToInt32(sample.quantity.doubleValue);
			}

			if (steps > 0) {
				this.resultsLabel.text += "FLIGHTS CLIMBED:" + steps;
			} else {
				this.resultsLabel.text += "No flights found.";
			}

			// all done
			reading = false;
		});
	}

	public void ReadSteps() {
		DateTimeOffset end = DateTimeOffset.UtcNow;
		DateTimeOffset start = end.AddMinutes(-10);

		this.healthStore.ReadSteps(start, end, delegate (double steps, Error error) {
			if (steps > 0) {
				this.resultsLabel.text += "total steps:" + steps;
			}
			else {
				this.resultsLabel.text += "No steps during this period.";
			}

			// all done
			reading = false;
		});
	}

	// A basic example of reading Quantity data.
	private void ReadQuantityData(HKDataType dataType, DateTimeOffset start, DateTimeOffset end) {
		string typeName = HealthKitDataTypes.GetIdentifier(dataType);
		Debug.LogFormat("reading {0} from {1} to {2}", typeName, start, end);
		double sum = 0;
		this.healthStore.ReadQuantitySamples(dataType, start, end, delegate(List<QuantitySample> samples, Error error) {
			if (samples.Count > 0) {
				Debug.Log("found " + samples.Count + " samples");
				bool cumulative = (samples[0].quantityType == QuantityType.cumulative);
				string text = "";
				foreach (QuantitySample sample in samples) {
					Debug.LogFormat("   - {0} : {1}", sample, sample.quantity.doubleValue);
					if (cumulative) {
						sum += Convert.ToInt32(sample.quantity.doubleValue);
						Debug.LogFormat("       - sum:{0}", sample.sumQuantity);
					} else {
						text = text + "- " + sample + "\n";
						Debug.LogFormat("       - min:{0} / max:{1} / avg:{2}", sample.minimumQuantity, sample.maximumQuantity, sample.averageQuantity);
					}
				}

				if (cumulative) {
					if (sum > 0) this.resultsLabel.text = typeName + ":" + sum;
				} else {
					this.resultsLabel.text = text;
				}
			} else {
				Debug.Log("found no samples");
			}


			// all done
			reading = false;
		});
	}
	
	// reading a Characteristic
	private void ReadCharacteristic(HKDataType dataType) {
		string typeName = HealthKitDataTypes.GetIdentifier(dataType);
		Debug.LogFormat("reading {0}", typeName);
		this.healthStore.ReadCharacteristic(dataType, delegate(Characteristic characteristic, Error error) {
			Debug.Log("FINISHED");
			string text = string.Format("{0} = {1}", dataType, characteristic);
			this.resultsLabel.text = text;

			// all done
			reading = false;
		});

	}

	// a generic example of reading correlation data. If you're interested in nutritional correlations, you'd probably tailor your delegate to the specific nutritional information you're looking at.
	private void ReadCorrelationData(HKDataType dataType, DateTimeOffset start, DateTimeOffset end) {
		this.healthStore.ReadCorrelationSamples(dataType, start, end, delegate(List<CorrelationSample> samples, Error error) {
			string text = "";
			foreach (CorrelationSample correlation in samples) {
				string str = "";
				foreach (Sample sample in correlation.objects) {
					QuantitySample s = (QuantitySample)sample;
					str += string.Format("[{0}:{1}] ", s.quantityType, s.quantity.doubleValue);
				}
				Debug.Log("- " + str);
				text = text + "- " + str + "\n";
			}
			this.resultsLabel.text = text;

			// all done
			reading = false;
		});
	}

	private void ReadWorkoutData(HKDataType dataType, DateTimeOffset start, DateTimeOffset end) {
		this.healthStore.ReadWorkoutSamples(WorkoutActivityType.FitnessGaming, start, end, delegate(List<WorkoutSample> samples, Error error) {
			string text = "";
			foreach (WorkoutSample sample in samples) {
				Debug.Log("   - " + sample);
				text = text + "- " + sample + "\n";
			}
			this.resultsLabel.text = text;

			// all done
			reading = false;
		});
	}

	// --------------------------

	public void ReadCombinedData() {
		if (!reading) {
			string selectedName = dropdown.options[dropdown.value].text;
			try {
				HKDataType dataType = (HKDataType)Enum.Parse(typeof(HKDataType), selectedName);
				reading = true;

				DateTimeOffset end = DateTimeOffset.UtcNow;
				// for this example, we'll read everything from the past 24 hours
				DateTimeOffset start = end.AddDays(-1);

				if (dataType <= HKDataType.HKQuantityTypeIdentifierUVExposure) {
					Debug.LogFormat("reading {0} from {1} to {2}", selectedName, start, end);
					double sum = 0;
					this.healthStore.ReadQuantitySamples(dataType, start, end, delegate (List<QuantitySample> samples, Error error) {
						if (samples.Count > 0) {
							Debug.Log("found " + samples.Count + " samples");
							bool cumulative = (samples[0].quantityType == QuantityType.cumulative);
							string text = "";
							foreach (QuantitySample sample in samples) {
								Debug.Log("   - " + sample);
								if (cumulative) sum += Convert.ToInt32(sample.quantity.doubleValue);
								else text = text + "- " + sample + "\n";
							}

							if (cumulative) {
								if (sum > 0) this.resultsLabel.text = selectedName + ":" + sum;
							}
							else {
								this.resultsLabel.text = text;
							}
						}
						else {
							Debug.Log("found no samples");
						}
						// all done
						reading = false;
					});
				}
				else {
					Debug.LogError("Combined data only works for quantity types");
				}
			}
			catch (ArgumentException) {
				// they just selected a divider; nothing to worry about
				Debug.LogFormat("{0} unrecognized", selectedName);
			}
		}
	}

	public void AddObserver() {
		this.resultsLabel.text = "starting observer query...\n";
		this.healthStore.AddObserverQuery(HKDataType.HKQuantityTypeIdentifierStepCount, delegate (List<Sample> samples, Error error) {
			string text = this.resultsLabel.text;
			int steps = 0;
			foreach (Sample sample in samples) {
				QuantitySample quantitySample = sample as QuantitySample;
				if (quantitySample != null) {
					steps += (int)quantitySample.quantity.doubleValue;
				}
			}
			text += string.Format("- {0} steps\n", steps);
			this.resultsLabel.text = text;
		});
	}


	/*
	public void ReadHealthDocument() {
		// since this can take a while, especially on older devices, I use this to avoid firing off multiple concurrent requests.
		Debug.Log("read data...");
		if (!reading) {
			reading = true;
			healthStore.ReadHealthDocuments(null, 10, true, (documents) => {
				string str = "";
				str += $"retreived {documents.Count} documents.\n";
				foreach (DocumentSample sample in documents) {
					CDADocument document = sample.document;
					str += $"- {document.title}";
				}
				this.resultsLabel.text = str;

				reading = false;
			});
		}
	}


	public void ReadClinicalData() {
		// since this can take a while, especially on older devices, I use this to avoid firing off multiple concurrent requests.
		Debug.Log("read data...");
		if (!reading) {
			reading = true;
			healthStore.ReadClinicalRecord(HKClinicalType.HKClinicalTypeIdentifierAllergyRecord, (records) => {
				string str = "";
				str += $"retreived {records.Count} records.\n";
				foreach (ClinicalRecord record in records) {
					str += $"- {record.clinicalType}: '{record.displayName}'";
				}
				this.resultsLabel.text = str;
				reading = false;
			});
		}
	}
	*/


	public void ReadPedometer() {
		DateTimeOffset start = DateTimeOffset.UtcNow;

		if (!reading) {
			int steps = 0;
			this.healthStore.BeginReadingPedometerData(start, delegate(List<PedometerData> data, Error error) {
				foreach (PedometerData sample in data) {
					steps += sample.numberOfSteps;
				}
				this.resultsLabel.text = string.Format("{0}", steps);
			});
			buttonLabel.text = "Stop reading";
			reading = true;
		} else {
			this.healthStore.StopReadingPedometerData();
			buttonLabel.text = "Start reading";
			reading = false;
		}
	}

	public void ReadStatistics() {
		DateTimeOffset now = DateTimeOffset.UtcNow;
		// for this example, we'll read everything from the past 24 hours
		DateTimeOffset start = now.AddDays(-1);


		healthStore.ReadStatistics(HKDataType.HKQuantityTypeIdentifierStepCount, start, now, StatisticsOptions.SeparateBySource, (statistics, error) => {
			string str = "";
			str += $"-     sum: {statistics.sumQuantity}\n";
			str += $"- minimum: {statistics.minimumQuantity}\n";
			str += $"- maximum: {statistics.maximumQuantity}\n";
			str += $"- average: {statistics.averageQuantity}\n";
			str += $"-  recent: {statistics.mostRecentQuantity}";
			resultsLabel.text = str;
			reading = false;
		});
	}

	public void ReadStatisticsCollection() {
		DateTimeOffset now = DateTimeOffset.UtcNow;
		DateTimeOffset anchor = now.AddDays(-1);
		TimeSpan interval = new TimeSpan(1, 0, 0);

		healthStore.ReadStatisticsCollection(HKDataType.HKQuantityTypeIdentifierStepCount, null, StatisticsOptions.None, anchor, interval, (collection, error) => {
			string str = "";
			str += $"statistics: {collection.statistics.Count}\n-----------------------\n";
			if (collection.statistics.Count > 0) {
				var statistics = collection.statistics[0];
				str += $"-     sum: {statistics.sumQuantity}\n";
				str += $"- minimum: {statistics.minimumQuantity}\n";
				str += $"- maximum: {statistics.maximumQuantity}\n";
				str += $"- average: {statistics.averageQuantity}\n";
				str += $"-  recent: {statistics.mostRecentQuantity}";
			}

			resultsLabel.text = str;
			reading = false;
		});
	}

	
	private void GenerateDummyData() {
		this.healthStore.GenerateDummyData(this.types);
	}

	private void GotSteps(int steps) {
		Debug.Log("*** READ STEPS:" + steps);
		reading = false;
	}

	private void ErrorOccurred(Error err) {
		this.errorLabel.text = err.localizedDescription;
	}

	// --- dummy data --------

	void OnGUI() {
		if (generateDummyData) {
			if (GUILayout.Button("[dummy data]")) {
				Debug.Log("Generating debug data...");
				this.GenerateDummyData();
			}
		}
	}
}

}