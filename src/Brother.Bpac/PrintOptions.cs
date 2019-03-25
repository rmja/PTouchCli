namespace Brother.Bpac
{
    public enum PrintOptions
    {
        Default = 0,
        AutoCut = 1,
        CutPause = 2,
        CutMark = 2,
        Mirroring = 4,
        Color = 8,
        Stamp = 128,
        HalfCut = 512,
        ChainPrint = 1024,
        TailCut = 2048,
        Quality = 65536,
        SpecialTape = 524288,
        HighSpeed = 16777216,
        HighResolution = 33554432,
        CutAtEnd = 67108864,
        Mono = 268435456,
        NoCut = 268435456,
        IdLabel = 268435456,
        Rfid = 536870912,
        Continue = 1073741824
    }
}
