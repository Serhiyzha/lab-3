using System;

namespace StringApp.BLL
{
    public enum ShiftDirectionModel
    {
        Forward,
        Backward
    }

    public class CustomStringModel
    {
        public string Value { get; set; } = string.Empty;
        public int Length => Value?.Length ?? 0;
        public int ShiftKey { get; set; }
        public ShiftDirectionModel ShiftDirection { get; set; }

        public string Encrypt()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;
            char[] buffer = Value.ToCharArray();
            int delta = (ShiftDirection == ShiftDirectionModel.Forward) ? ShiftKey : -ShiftKey;
            for (int i = 0; i < buffer.Length; i++) buffer[i] = (char)(buffer[i] + delta);
            return new string(buffer);
        }

        public string Decrypt()
        {
            if (string.IsNullOrEmpty(Value)) return string.Empty;
            char[] buffer = Value.ToCharArray();
            int delta = (ShiftDirection == ShiftDirectionModel.Forward) ? -ShiftKey : ShiftKey;
            for (int i = 0; i < buffer.Length; i++) buffer[i] = (char)(buffer[i] + delta);
            return new string(buffer);
        }
    }
}