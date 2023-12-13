//
//  BEHealthKit+clinicalRecords.h
//  Unity-iPhone
//
//  Created by greay on 3/9/20.
//

#import <Foundation/Foundation.h>

#import "BEHealthKit.h"

/*! @brief      Category for reading clinical records.
    @details    Due to the sensitive nature of clinical records, Apple is very strict about approving apps that reference the clinical record API.
                In light of that, this category is entirely optional, and I recommend not including it in the project at all unless you plan on
                utilizing it.
 */
@interface BEHealthKit (clinicalRecords)

/*! @brief                  read clinical records
	@details                A streamlined method to perform sample queries on clinical types.
	@param sampleType       the type of sample to read.
	@param resultsHandler	Called when the query finishes executing. If unsuccessful, error contains information about what went wrong, otherwise it will be set to nil.
 */
- (void)readHealthRecords:(HKSampleType *)sampleType resultsHandler:(void (^)(id result, NSError *error))resultsHandler;

@end

// hooks for external interface
// ----------------------------
void _ReadHealthRecord(char *identifier);
