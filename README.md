# MemoryBuffer
Wrapper class for System.IO.MemoryStream for writing specific data types.

Supported Types: bool, byte, int, float, double, string, byte[].
 - Strings are null-terminated.
 - Byte Array will first write their length as an Int32, then write the array.
