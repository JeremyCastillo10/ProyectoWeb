using Braintree;

namespace ProyectoWeb.Utilidades.BrainTree
{
    public interface IBraintTreeGate
    {
        IBraintreeGateway CreateGateway();
        IBraintreeGateway GetGateway();
    }
}
