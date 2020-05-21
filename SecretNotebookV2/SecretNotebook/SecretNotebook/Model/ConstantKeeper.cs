using SecretNotebook.View;
using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Model
{
    //на данный момент нет задачи использовать config
    public static class ConstantKeeper
    {
        public static Area CurrentArea = null;
        
        internal static ModelSource CurrentSource = null;

        public static string PathToNotes = @"notes.nts";

        public static string PathToKeys = @"hash.key";

        internal static byte[] Entropy = new byte[0];

        internal static byte[] IV = new byte[16];

        internal static byte[] Key;

        public static void SetKey(byte[] key)
        {
            Key = key;
        }

        public static void SetIV(byte[] iv)
        {
            IV = iv;
        }
    }
}
