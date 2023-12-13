//
//  BEHealthKit.h
//  Unity-iPhone
//
//  Created by greay on 3/25/15.
//
//

#import <Foundation/Foundation.h>
#import <HealthKit/HealthKit.h>
#import "BEPedometer.h"

#define BEHK_ERROR_UNKNOWN_DATATYPE 2001
#define BEHK_ERROR_HK_UNAVAILABLE 1001
#define BEHK_ERROR_NO_DEFAULT_UNIT 1003
#define BEHK_ERROR_TYPE_NOT_FOUND 1404
#define BEHK_ERROR_FEATURE_UNAVAILABLE 1005

/*! @brief Handles HealthKit requests from Unity.
 */
@interface BEHealthKit : NSObject

@property HKHealthStore *healthStore; /*!< @brief The HKHealthStore object */
@property NSString *controllerName;	/*!< @brief name of the GameObject to send messages to */
@property (nonatomic, strong) BEPedometer *pedometer; /*!< @brief The BEPedometer object */
@property (nonatomic, strong) NSDictionary *longRunningQueries;

/*! @brief returns the shared BEHealthKit object */
+ (instancetype)sharedHealthKit;


/*! @brief                   returns the authorization status for the given data type.
	@param dataTypeString    HealthKit datatype identifiers to query.
 */
- (int)authorizationStatusForType:(NSString *)dataTypeString;

/*! @brief                   brings up the system health data authorization panel.
	@details                 Wrapper around the HealthKit -requestAuthorizationToShareTypes:readTypes:completion method.
	@param readIdentifiers   array of HealthKit datatype identifiers to read.
	@param writeIdentifiers  array of HealthKit datatype identifiers to write.
	@param completion        called after the user responds to the request. If success is false, error contain information about what went wrong, otherwise it will be set to nil.
 */
- (void)authorizeHealthKitToRead:(NSArray *)readIdentifiers write:(NSArray *)writeIdentifiers completion:(void (^)(bool success, NSError *error))completion;


/*! @brief			Sends an error back to Unity.
	@details		Converts the error to XML (See NSError+XML.h), and calls ErrorOccurred() on the HealthStore GameObject.
	@param error	the error.
 */
- (void)errorOccurred:(NSError *)error;


/*! @brief				Returns the default unit for a sample type.
	@details			I try to be somewhat intelligent about this (temperature, for example, will be returned in the device's locale, so you don't need to worry about converting it before displaying to the user.
						That said, if you want to change some of these defaults this is the place to do it. Will return nil if there is no default unit, but this shouldn't happen.
	@param sampleType	The sample type in question. Can be any sample type supported by HealthKit.
 */
- (HKUnit *)defaultUnitForSampleType:(HKSampleType *)sampleType;

- (void)addLongRunningQuery:(HKQuery *)query forType:(HKSampleType *)sampleType;

@end


// hooks for external interface
// ----------------------------
void _InitializeNative(char *controllerName);
void _Authorize(char *dataTypesString);
int _AuthorizationStatusForType(char *dataTypeString);
BOOL _IsHealthDataAvailable();

// internal
NSArray *parseTransmission(char *dataTypesString);

