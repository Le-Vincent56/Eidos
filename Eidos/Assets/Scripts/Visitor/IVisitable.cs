namespace Eidos.Visitor
{
    public interface IVisitable
    {
        void Accept(IVisitor visitor);
    }
}
