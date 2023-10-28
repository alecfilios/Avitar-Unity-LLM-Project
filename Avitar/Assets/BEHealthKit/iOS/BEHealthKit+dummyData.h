//
//  BEHealthKit+dummyData.h
//  Unity-iPhone
//
//  Created by greay on 5/16/15.
//
//

#import "BEHealthKit.h"

/*!	@brief Category for generating (somewhat reasonable) dummy data 
 */
@interface BEHealthKit (dummyData)

/*! @brief generates dummy data for all authorized data types. */
- (void)generateDummyData;

@end

void _GenerateDummyData();
