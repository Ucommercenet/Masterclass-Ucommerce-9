using System;
using System.Linq;
using UCommerce.EntitiesV2;

namespace MyUCommerceApp.Integration
{
	class Program
	{
		static void Main(string[] args)
		{
            var firstOrder = PurchaseOrder.All().First();
        }
	}
}
