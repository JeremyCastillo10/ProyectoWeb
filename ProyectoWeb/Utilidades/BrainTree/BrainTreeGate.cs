using Braintree;
using Microsoft.Extensions.Options;
using ProyectoWeb.Models;
using System.Security.Cryptography.X509Certificates;

namespace ProyectoWeb.Utilidades.BrainTree
{
    public class BrainTreeGate : IBraintTreeGate
    {
        public BrainTreeSettings _options { get; set; }
        public IBraintreeGateway brainTreeGateway { get; set; }
        public BrainTreeGate(IOptions<BrainTreeSettings> options)
        {
            _options = options.Value;
        }
        public IBraintreeGateway CreateGateway()
        {
            if (_options.Environment == null || _options.MerchantId == null || _options.PublicKey == null || _options.PrivateKey == null)
            {
                _options.Environment = "sandbox";
                _options.MerchantId = "8vghsspyn9b3k2yy";
                _options.PublicKey = "cgsqr6cpmc4b3wy5";
                _options.PrivateKey = "8afea6bfeafff1fe89afdcb8b9bd45d2";
            }
            return new BraintreeGateway(_options.Environment, _options.MerchantId, _options.PublicKey, _options.PrivateKey);
        }

        public IBraintreeGateway GetGateway()
        {
            if (brainTreeGateway == null)
            {
                brainTreeGateway = CreateGateway();
            }

            return brainTreeGateway;
        }

    }
}
