//
//  BEHealthKit+read.h
//  Unity-iPhone
//
//  Created by greay on 10/10/19.
//

#import <Foundation/Foundation.h>


#import "BEHealthKit.h"

/*! @brief					 Methods for reading health data
	@details				 Read methods for the primary between Unity & the native HealthKit API.
 */
@interface BEHealthKit (read)

/*! @brief					 read quantity, category or correlation samples.
	@details				 Executes a query with -initWithSampleType:predicate:limit:sortDescriptors:resultsHandler:. Limit will be set to no limit, and they will be sorted by startDate, in ascending order.
	@param sampleType		 the type of sample to read.
	@param startDate		 the starting limit for the query.
	@param endDate			 the end date.
	@param resultsHandler	 Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readSamples:(HKSampleType *)sampleType fromDate:(NSDate *)startDate toDate:(NSDate *)endDate resultsHandler:(void (^)(NSArray *results, NSError *error))resultsHandler;


/*! @brief					read a characteristic.
	@details				Characteristics are things that don't change over time, like blood type or birth date.
	@param characteristic	The characteristic to read.
	@param resultsHandler	Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readCharacteristic:(HKCharacteristicType *)characteristic resultsHandler:(void (^)(id result, NSError *error))resultsHandler;


/*! @brief					read workout samples
	@details				...
	@param activity		The activity type to read. See [HKWorkoutActivityType documentation](https://developer.apple.com/library/prerelease/ios/documentation/HealthKit/Reference/HealthKit_Constants/index.html#//apple_ref/c/tdef/HKWorkoutActivityType)
	@param startDate		the starting limit for the query.
	@param endDate			the end date.
	@param resultsHandler	Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readSamplesForWorkoutActivity:(HKWorkoutActivityType)activity fromDate:(NSDate *)startDate toDate:(NSDate *)endDate resultsHandler:(void (^)(NSArray *results, NSError *error))resultsHandler;



/*! @brief                  perform a statistics query
	@details                A query that performs statistical calculations over a set of matching quantity samples, and returns the results. See [HKStatisticsQuery](https://developer.apple.com/documentation/healthkit/hkstatisticsquery).
	@param quantityType     the type of sample to read.
	@param predicate        a predicate that limits the results of the query
	@param options          a single option that defines the type of calculation to be performed on the data
	@param resultsHandler	Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readStatisticsForQuantityType:(HKQuantityType *)quantityType predicate:(NSPredicate *)predicate options:(HKStatisticsOptions)options resultsHandler:(void (^)(id result, NSError *error))resultsHandler;


/*! @brief                     read health statistics collection
	@details                   A query that performs multiple statistics queries over a series of fixed-length time intervals, and returns the results. See [HKStatisticsCollectionQuery](https://developer.apple.com/documentation/healthkit/hkstatisticscollectionquery)
	@param quantityType        the type of sample to read.
	@param predicate           a predicate that limits the results of the query
	@param options             a single option that defines the type of calculation to be performed on the data
	@param anchorDate          the anchor date for the collection's time intervals
	@param interval            the date components that determine the time interval for each statistic in this collection
	@param resultsHandler	   Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readStatisticsCollectionForQuantityType:(HKQuantityType *)quantityType predicate:(NSPredicate *)predicate options:(HKStatisticsOptions)options anchorDate:(NSDate *)anchorDate intervalComponents:(NSDateComponents *)interval resultsHandler:(void (^)(id result, NSError *error))resultsHandler;


/*! @brief                      read health document
	@details                    A query that returns a snapshot of all matching documents currently saved in the HealthKit store. See [HKDocumentQuery](https://developer.apple.com/documentation/healthkit/hkdocumentquery).
	@param documentType         Currently, only HKDocumentTypeIdentifierCDA is supported.
	@param predicate            a predicate that limits the results of the query
	@param limit                the maximum number of documents to return
	@param sortDescriptors      an array of sort decsriptors
	@param includeDocumentData  send true to include all document data. send false to just return a summary.
	@param resultsHandler	    Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readDocumentOfType:(HKDocumentType *)documentType predicate:(NSPredicate *)predicate limit:(NSUInteger)limit sortDescriptors:(NSArray<NSSortDescriptor *> *)sortDescriptors includeDocumentData:(BOOL)includeDocumentData resultsHandler:(void (^)(id result, BOOL done, NSError *error))resultsHandler;

- (void)beginBackgroundObserverQuery:(HKQuantityTypeIdentifier)identifierString;

@end


// hooks for external interface
// ----------------------------
void _ReadQuantity(char *identifier, char *startDateString, char *endDateString, bool combineSamples);
void _ReadCategory(char *identifier, char *startDateString, char *endDateString);
void _ReadCharacteristic(char *identifier);
void _ReadCorrelation(char *identifier, char *startDateString, char *endDateString, bool combineSamples);
void _ReadWorkout(int activityID, char *startDateString, char *endDateString, bool combineSamples);
void _BeginObserverQuery(char *identifier);
void _StopObserverQuery(char *identifier);

void _ReadCombinedQuantityStatistics(char *identifier, char *startDateString, char *endDateString);
void _ReadStatistics(char *identifier, char *startDateString, char *endDateString, char *optionsString);
void _ReadStatisticsCollection(char *identifier, char *predicateString, char *optionsString, char *anchorStamp, char *intervalString);

void _ReadDocument(/* char *documentTypeString, */ char *predicateString, int limit, char *sort, bool includeData);

void _EnableBackgroundDelivery(char *identifier, int frequency);

// ---

void _ReadPedometer(char *startDateString, char *endDateString);
void _StartReadingPedometerFromDate(char *startDateString);
void _StopReadingPedometer();
