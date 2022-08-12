using System.Runtime.Serialization;

namespace DependencyInjection.Exceptions;

[Serializable]
public class CannotResolveAnyConstructorException : Exception
{
    //
    // For guidelines regarding the creation of new exception types, see
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    // and
    //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    //

    public CannotResolveAnyConstructorException()
    {
    }

    public CannotResolveAnyConstructorException(string message) : base(message)
    {
    }

    public CannotResolveAnyConstructorException(string message, Exception inner) : base(message, inner)
    {
    }

    protected CannotResolveAnyConstructorException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}