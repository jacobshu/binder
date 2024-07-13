// the variable shop

// stringy
string str = "The string type uses double quotes";
char chr = 'c';

// 1 byte: 0b00000000, 0x00
byte minByte = 0;
byte maxByte = 255;

sbyte minSbyte = -128;
sbyte maxSbyte = 127;

// 2 bytes: 0b00000000_00000000, 0x00_00
short minShort = -32_768;
short maxShort = 32_767;

ushort minUshort = 0;
ushort maxUshort = 65_535;

// 4 bytes: 0b00000000_00000000_000000000_000000000, 0x00_00_00_00
int minInt = -2_147_483_648;
int maxInt = 2_147_483_647;

uint minUint = 0;
uint maxUint = 4_294_967_295;

// 8 bytes: 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000
//          0x00_00_00_00_00_00_00_00
long minLong = -9_223_372_036_854_775_808 ;
long maxLong = 9_223_372_036_854_775_807;

ulong minUlong = 0;
ulong maxUlong = 18_446_744_073_709_551_615;

// floating point numbers
//             
// 4 bytes: 0b00000000_00000000_000000000_000000000, 0x00_00_00_00
float numFloat = 1.123f;

// 8 bytes: 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000
//          0x00000000_00000000
double numDouble = 0.0456;

//             sign & exp  ┌-------- number --------┐ 
// 16 bytes: 0x 00000000 _ 00000000_00000000_00000000
decimal numDecimal = 1e11m;


Console.WriteLine("a char is assigned with single quotes: " + chr);
Console.WriteLine("a string can be any length of chars. Like: " + str);

Console.WriteLine("the byte type represents the smallest range of numbers with a single byte (8 bits) from " + minByte + " to " + maxByte);
Console.WriteLine("the sbyte type rpresents the same range as a byte but can be signed for negative numbers, from " + minSbyte + " to " + maxSbyte);

Console.WriteLine("the short type uses 2 bytes for representing numbers from " + minShort + " to " + maxShort);
Console.WriteLine("the ushort type is an unsigned short representing numbers from " + minUshort + " to " + maxUshort);

Console.WriteLine("an int type is a signed 4 byte representation of numbers in the range " + minInt + " - " + maxInt);
Console.WriteLine("an uint type is an unsigned 4 byte representation of numbers in the range " + minUint + " - " + maxUint);

Console.WriteLine("a long type is a signed 8 byte representation of numbers in the range " + minLong + " - " + maxLong);
Console.WriteLine("a ulong type is an unsigned 8 byte representation of numbers in the range " + minUlong + " - " + maxUlong);

Console.WriteLine("a float type can represent a wider range of numbers since it reserves 8 of its 32 bits for the exponent (1 bit for the sign, 23 for the mantissa): " + numFloat);
Console.WriteLine("a double is a 8 byte floating point number that represents both a larger absolute range and greater precision than a float: " + numDouble);
Console.WriteLine("a decimal type represents a smaller absolute range than a float but with much greater precision as 96 of the 128 bits are reserved for the mantissa: " + numDecimal);
