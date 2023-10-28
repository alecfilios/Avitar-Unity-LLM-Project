//
//  NSDate+bridge.m
//  Unity-iPhone
//
//  Created by greay on 3/28/15.
//
//

#import "NSDate+bridge.h"
#import	"NSNumber+bridge.h"

@implementation NSDate (conversion)

+ (instancetype)dateFromToken:(NSNumber *)n
{
	NSDate *date = [NSDate dateWithTimeIntervalSince1970:(NSTimeInterval)[n doubleValue]];
	// NSLog(@"date from token '%@' => %@", n, date);
	return date;
}

+ (instancetype)dateFromBridgeString:(char *)stamp
{
	NSString *str = [NSString stringWithCString:stamp encoding:NSUTF8StringEncoding];
	NSNumber *n = [[NSNumber bridgeFormatter] numberFromString:str];
	// NSLog(@"reading bridge string '%s' => '%@' => %@", stamp, str, n);
	return [self dateFromToken:n];
}

- (NSString *)bridgeToken
{
	NSTimeInterval token = [self timeIntervalSince1970];
	return [[NSNumber bridgeFormatter] stringFromNumber:@(token)];
}

@end
