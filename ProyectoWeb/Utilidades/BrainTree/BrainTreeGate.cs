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
                _options.MerchantId = "qky778sx973ns73q";
                _options.PublicKey = "kjfqv5bxnvfm2497";
                _options.PrivateKey = "a9a45d0af8475a5664f0c99789bab3e7";
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
