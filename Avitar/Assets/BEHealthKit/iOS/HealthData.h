//
//  HealthData.h
//  Unity-iPhone
//
//  Created by greay on 3/28/15.
//
//

#import <Foundation/Foundation.h>
#import <HealthKit/HealthKit.h>
#import <CoreMotion/CoreMotion.h>

/*! @brief Helper class to generate XML from HealthKit data, for sending to Unity
 */
@interface HealthData : NSObject

+ (NSString *)XMLFromSamples:(NSArray *)samples datatype:(HKObjectType *)datatype error:(NSError *)error;          /*!< @brief generate XML from an array of unknown Samples */

+ (NSString *)XMLFromQuantitySamples:(NSArray *)quantitySamples datatype:(NSString *)datatype error:(NSError *)error;          /*!< @brief generate XML from an array of QuantitySamples */
+ (NSString *)XMLFromCategorySamples:(NSArray *)categorySamples datatype:(NSString *)datatype error:(NSError *)error;          /*!< @brief generate XML from an array of CategorySamples */
+ (NSString *)XMLFromCorrelationSamples:(NSArray *)correlationSamples datatype:(NSString *)datatype error:(NSError *)error;    /*!< @brief generate XML from an array of CorrelationSamples */
+ (NSString *)XMLFromCharacteristic:(id)characteristic datatype:(NSString *)datatype error:(NSError *)error;                   /*!< @brief generate XML for a Characteristic */
+ (NSString *)XMLFromWorkoutSamples:(NSArray *)workoutSamples workoutType:(int)workoutType error:(NSError *)error;             /*!< @brief generate XML from an array of WorkoutSamples */


+ (NSString *)XMLFromCombinedTotal:(double)total datatype:(NSString *)datatype error:(NSError *)error;                         /*!< @brief generate XML from an a total of combined QuantitySamples */
+ (NSString *)XMLFromStatistics:(HKStatistics *)statistics datatype:(HKQuantityType *)datatype error:(NSError *)error;         /*!< @brief generate XML from an HKStatistics object */
+ (NSString *)XMLFromStatisticsCollection:(HKStatisticsCollection *)collection datatype:(HKQuantityType *)sampleType anchorDate:(NSDate *)anchorDate interval:(NSDateComponents *)interval error:(NSError *)error; /*!< @brief generate XML from an HKStatisticsCollection */

+ (NSString *)XMLFromHealthDocuments:(id)document error:(NSError *)error;                                                      /*!< @brief generate XML from a health document */

+ (NSString *)XMLFromPedometerData:(CMPedometerData *)data error:(NSError *)error;                                             /*!< @brief generate XML from CMPedometerData */

@end

/*! @brief Serialization category for the HKObject class & subclasses
 */
@interface HKObject (serialization)
- (id)be_serializable; /*!< @brief returns a serializable dictionary representation of the object */
@end

/*! @brief Serialization category for the HKObjectType class
 */
@interface HKObjectType (serialization)
- (id)be_serializable; /*!< @brief returns a serializable dictionary representation of the object */
@end
