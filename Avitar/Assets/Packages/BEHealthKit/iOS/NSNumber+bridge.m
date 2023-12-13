//
//  NSNumber+bridge.m
//  Unity-iPhone
//
//  Created by greay on 12/10/20.
//
//

#import "NSNumber+bridge.h"

@implementation NSNumber (conversion)

+ (NSNumberFormatter *)bridgeFormatter
{
	static NSNumberFormatter *format = nil;
	static dispatch_once_t onceToken;
	dispatch_once(&onceToken, ^{
		format = [[NSNumberFormatter alloc] init];
		format.numberStyle = NSNumberFormatterDecimalStyle;
		format.usesGroupingSeparator = false;
		format.locale = [NSLocale localeWithLocaleIdentifier:@"en_US"];
	});
	return format;
}

+ (NSNumberFormatter *)localizedFormatter
{
	static NSNumberFormatter *format = nil;
	static dispatch_once_t onceToken;
	dispatch_once(&onceToken, ^{
		format = [[NSNumberFormatter alloc] init];
		format.numberStyle = NSNumberFormatterDecimalStyle;
		format.locale = [NSLocale currentLocale];
	});
	return format;
}


@end
