using System;
using System.Collections.Generic;
using System.Text;

namespace SecretNotebook.Model
{
    //на данный момент нет задачи использовать config
    public static class ConstantKeeper
    {
        public const string PathToNotes = @"notes.nts";

        public const string PathToKeys = @"hash.key";

        internal static byte[] Entropy = new byte[0];

        //?
        // BlockSize / 8
        internal static byte[] IV = new byte[16];

        internal static byte[] Key;
    }
}
