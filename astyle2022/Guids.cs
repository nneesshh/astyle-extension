// Guids.cs
// MUST match guids.h

using System;

namespace AStyleExtension {
    static class GuidList {
        public const string GuidPkgString = "63a7f3e9-2674-4c53-ae69-015011800a3e";
        public const string GuidCmdSetString = "334dc68f-6dce-4baa-86df-b9e2856fdccd";

        public static readonly Guid GuidCmdSet = new Guid(GuidCmdSetString);
    };
}