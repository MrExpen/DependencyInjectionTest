using System.Runtime.Serialization;

namespace DependencyInjection.Exceptions;

[Serializable]
public class TypeAlreadyRegisteredException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //
    
    public TypeAlreadyRegisteredException()
    {
    }

    public TypeAlreadyRegisteredException(string message) : base(message)
    {
    }

    public TypeAlreadyRegisteredException(string message, Exception inner) : base(message, inner)
    {
    }

    protected TypeAlreadyRegisteredException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
