// the variable shop

// strings
string str = "The string type uses double quotes";
char chr = 'c';
Console.WriteLine("a char is assigned with single quotes: " + chr);
Console.WriteLine("a string can be any length of chars. Like: " + str);

// 1 byte: 0b00000000, 0x00
byte numByte = 0;
Console.WriteLine("the byte type represents the smallest range of numbers with a single byte (8 bits)");
Console.WriteLine("Minimum byte value: " + numByte);
numByte = byte.MaxValue;
Console.WriteLine("Maximum byte value: " + numByte);

sbyte numSbyte = -128;
Console.WriteLine("the sbyte type rpresents the same range as a byte but can be signed for negative numbers");
Console.WriteLine("the minimum sbyte value: " + numSbyte);
numSbyte = sbyte.MaxValue;
Console.WriteLine("the maximum sbyte value: " + numSbyte);

// 2 bytes: 0b00000000_00000000, 0x00_00
short numShort = short.MinValue;
Console.WriteLine("the short type uses 2 bytes for representing whole numbers.");
Console.WriteLine("Minimum short value: " + numShort);
numShort = short.MaxValue;
Console.WriteLine("Maximum short value: " + numShort);

ushort numUshort = ushort.MinValue;
Console.WriteLine("the ushort type is an unsigned, 2 byte short representing whole numbers.");
Console.WriteLine("Minimum ushort value: " + numUshort);
numUshort = ushort.MaxValue;
Console.WriteLine("Maximum ushort value: " + numUshort);

// 4 bytes: 0b00000000_00000000_000000000_000000000, 0x00_00_00_00
int numInt = int.MinValue;
Console.WriteLine("an int type is a signed 4 byte representation of whole numbers.");
Console.WriteLine("Minimum int value: " + numInt);
numInt = int.MaxValue;
Console.WriteLine("Maximum int value: " + numInt);

uint numUint = uint.MinValue;
Console.WriteLine("a uint type is an unsigned 4 byte representation of whole numbers.");
Console.WriteLine("Minimum uint value: " + numUint);
numUint = uint.MaxValue;
Console.WriteLine("Maximum uint value: " + numUint);


// 8 bytes: 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000
//          0x00000000_00000000
long numLong = long.MinValue;
Console.WriteLine("a long type is a signed 8 byte representation of whole numbers.");
Console.WriteLine("Minimum long value: " + numLong);
numLong = long.MaxValue;
Console.WriteLine("Maximum long value: " + numLong);

ulong numUlong = ulong.MinValue;
Console.WriteLine("a ulong type is an unsigned 8 byte representation of floating point numbers.");
Console.WriteLine("Minimum ulong value: " + numUlong);
numUlong = ulong.MaxValue;
Console.WriteLine("Maximum ulong value: " + numUlong);

// floating point numbers
//             
// 4 bytes: 0b00000000_00000000_000000000_000000000, 0x00_00_00_00
float numFloat = float.MinValue;
Console.WriteLine("a float type can represent a wider range of numbers since it reserves 8 of its 32 bits for the exponent (1 bit for the sign, 23 for the mantissa).");
Console.WriteLine("Minimum float value: " + numFloat);
numFloat = float.MinValue;
Console.WriteLine("Maximum float value: " + numFloat);


// 8 bytes: 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_00000000
//          0x00000000_00000000
double numDouble = double.MinValue;
Console.WriteLine("a double is a 8 byte floating point number that represents both a larger absolute range and greater precision than a float.");
Console.WriteLine("Minimum double value: " + numDouble);
numDouble = double.MaxValue;
Console.WriteLine("Maximum double value: " + numDouble);


//             sign & exp  ┌-------- number --------┐ 
// 16 bytes: 0x 00000000 _ 00000000_00000000_00000000
decimal numDecimal = decimal.MinValue;
Console.WriteLine("a decimal type represents a smaller absolute range than a float but with much greater precision as 96 of the 128 bits are reserved for the mantissa.");
Console.WriteLine("Minimum decimal value: " + numDecimal);
numDecimal = decimal.MaxValue;
Console.WriteLine("Maximum decimal value: " + numDecimal);

bool boolean = true;
Console.WriteLine("a bool type represents either the true or false values.");
Console.WriteLine("initialized and assigned true: " + boolean);
boolean = false;
Console.WriteLine("bool assigned false: " + boolean);

