# Fody Unsafe Issue

This solution contains two projects, one is a Fody plugin, which does nothing:

```
public void Execute()
{
    LogInfo("Weaving...");
}
```

Another project `Test` is a C# console application, which uses `Test.Fody` to weave it. In the `FodyWeavers.xml` it is set to do `VerifyAssembly`.

```
<?xml version="1.0" encoding="utf-8" ?>
<Weavers VerifyAssembly="true">
    <Test/>
</Weavers>
```

The Fody plugin does nothing, but the `Test` project contains some unsafe stuff:

```
class Program
{
    unsafe private struct IndicesAndValue
    {
        public double Value;
        public fixed int Indices[8];
    }

    public static unsafe void MakeIndicesAndValue()
    {
        var theStruct = new IndicesAndValue();
        theStruct.Value = 9.9;
        theStruct.Indices[0] = 8;
    }

    static void Main(string[] args)
    {
        MakeIndicesAndValue();
    }
}
```

If you build the project, the assembly verification throws error:

```
1>------ Rebuild All started: Project: Test.Fody, Configuration: Debug Any CPU ------
1>  Test.Fody -> C:\Users\solom\Documents\Projects\FodyUnsafeIssue\packages\Test.Fody\Test.Fody.dll
2>------ Rebuild All started: Project: Test, Configuration: Debug Any CPU ------
2>    Fody: Fody (version 1.29.4.0) Executing
2>      Fody/Test:   Weaving...
2>    Fody:   Finished Fody 29ms.
2>  Test -> C:\Users\solom\Documents\Projects\FodyUnsafeIssue\Test\bin\Debug\Test.exe
2>    Fody:   Verifying assembly
2>MSBUILD : error : Fody: PEVerify of the assembly failed.
2>MSBUILD : error :
2>MSBUILD : error : Microsoft (R) .NET Framework PE Verifier.  Version  4.0.30319.0
2>MSBUILD : error : Copyright (c) Microsoft Corporation.  All rights reserved.
2>MSBUILD : error :
2>MSBUILD : error : [IL]: Error: [C:\Users\solom\Documents\Projects\FodyUnsafeIssue\Test\bin\Debug\Test.exe : Test.Program::MakeIndicesAndValue][offset 0x00000025][found address of Int32] Expected numeric type on the stack.(Error: 0x8013185D)
2>MSBUILD : error : 1 Error(s) Verifying C:\Users\solom\Documents\Projects\FodyUnsafeIssue\Test\bin\Debug\Test.exe
2>MSBUILD : error :
2>    Fody:   Finished verification in 31ms.
========== Rebuild All: 1 succeeded, 1 failed, 0 skipped ==========
```
