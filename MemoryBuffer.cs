using System.Text;

namespace System.IO {
    /// <summary>Wrapper class for MemoryStream.</summary>
    public class MemoryBuffer {
        /// <summary>The underlying MemoryStream.</summary>
        public MemoryStream Memory { get; private set; }

        /// <summary>The current length of the MemoryStream.</summary>
        public long Length { get { return Memory.Length; } }

        /// <summary>The current position of the MemoryStream.</summary>
        public long Position { get { return Memory.Position; } }

        /// <summary>Creates a new MemoryBuffer.</summary>
        public MemoryBuffer() {
            Memory = new MemoryStream();
        }

        /// <summary>Creates a new MemoryBuffer with the capacity of the underlying MemoryStream.</summary>
        /// <param name="capacity">Capacity of the underlying MemoryStream.</param>
        public MemoryBuffer(int capacity) {
            Memory = new MemoryStream(capacity);
        }

        /// <summary>Writes a bool to the MemoryStream.</summary>
        /// <param name="value">Boolean value to write.</param>
        public void Write(bool value) {
            Memory.WriteByte(Convert.ToByte(value));
        }

        /// <summary>Writes a byte to the MemoryStream.</summary>
        /// <param name="value">Byte value to write.</param>
        public void Write(byte value) {
            Memory.WriteByte(value);
        }

        /// <summary>Writes a int to the MemoryStream.</summary>
        /// <param name="value">Integer value to write.</param>
        public void Write(int value) {
            byte[] bytes = BitConverter.GetBytes(value);
            Memory.Write(bytes, 0, bytes.Length);
        }

        /// <summary>Writes a float to the MemoryStream.</summary>
        /// <param name="value">Float value to write.</param>
        public void Write(float value) {
            byte[] bytes = BitConverter.GetBytes(value);
            Memory.Write(bytes, 0, bytes.Length);
        }

        /// <summary>Writes a double to the MemoryStream.</summary>
        /// <param name="value">Double value to write.</param>
        public void Write(double value) {
            byte[] bytes = BitConverter.GetBytes(value);
            Memory.Write(bytes, 0, bytes.Length);
        }

        /// <summary>Writes a null-terminated string to the MemoryStream.</summary>
        /// <param name="value">String value to write.</param>
        public void Write(string value) {
            foreach(char c in value)
                Memory.Write(BitConverter.GetBytes(c), 0, sizeof(char));
            Memory.Write(BitConverter.GetBytes(char.MinValue), 0, sizeof(char));
        }

        /// <summary>Writes a byte array to the MemoryStream.</summary>
        /// <param name="value">Byte array to write.</param>
        public void Write(byte[] value) {
            Write(value.Length);
            Memory.Write(value, 0, value.Length);
        }

        /// <summary>Reads a bool from the MemoryStream.</summary>
        /// <param name="value">Variable to write the boolean value to.</param>
        public void Read(out bool value) =>
            value = Convert.ToBoolean(Memory.ReadByte());

        /// <summary>Reads a byte from the MemoryStream.</summary>
        /// <param name="value">Variable to write the byte value to.</param>
        public void Read(out byte value) =>
            value = Convert.ToByte(Memory.ReadByte());

        /// <summary>Reads a int from the MemoryStream.</summary>
        /// <param name="value">Variable to write the integer value to.</param>
        public void Read(out int value) {
            byte[] bytes = new byte[sizeof(int)];
            Memory.Read(bytes, 0, bytes.Length);
            value = Convert.ToInt32(bytes);
        }

        /// <summary>Reads a null-terminated string from the MemoryStream.</summary>
        /// <param name="value">Variable to write the string value to.</param>
        public void Read(out string value) {
            StringBuilder str = new StringBuilder();
            byte[] chr = new byte[sizeof(char)];
            char? c = null;

            while(c != char.MinValue) {
                Memory.Read(chr, 0, chr.Length);
                c = BitConverter.ToChar(chr, 0);
                str.Append(c);
            }

            value = str.ToString();
        }

        /// <summary>Reads a byte array from the MemoryStream.</summary>
        /// <param name="value">Variable to write the byte array to.</param>
        public void Read(out byte[] value) {
            int length;
            Read(out length);

            value = new byte[length];
            Memory.Read(value, 0, length);
        }
    }
}
