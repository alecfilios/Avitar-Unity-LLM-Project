//
//  HealthData+clinicalRecords.h
//  Unity-iPhone
//
//  Created by greay on 3/9/20.
//

#import <Foundation/Foundation.h>
#import "HealthData.h"

/*! @brief Helper category for clinical records serialization
 */
@interface HealthData (clinicalRecords)

+ (NSString *)XMLFromClinicalRecords:(id)records datatype:(HKSampleType *)datatype; /*!< @brief generate XML from an array of clinical records */

@end
