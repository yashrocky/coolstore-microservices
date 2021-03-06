using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreKit.Infrastructure.AspNetCore.Authz;
using VND.CoolStore.Services.Cart.v1.Grpc;
using static VND.CoolStore.Services.Cart.v1.Grpc.CartService;

namespace VND.CoolStore.Services.WebAggregator.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/carts")]
    public class CartController : ControllerBase
    {
        private readonly CartServiceClient _serviceClient;

        public CartController(CartServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        [HttpGet]
        [Route("{id}")]
        //[Auth(Policy = "access_cart_api")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _serviceClient.GetCartAsync(new GetCartRequest
                {
                    CartId = id.ToString()
                })
            );
        }

        [HttpPost]
        //[Auth(Policy = "access_cart_api")]
        public async Task<IActionResult> Create(InsertItemToNewCartRequest request)
        {
            return Ok(await _serviceClient.InsertItemToNewCartAsync(request));
        }

        [HttpPut]
        //[Auth(Policy = "access_cart_api")]
        public async Task<IActionResult> Put(UpdateItemInCartRequest request)
        {
            return Ok(await _serviceClient.UpdateItemInCartAsync(request));
        }

        [HttpPut]
        [Route("{cartId:guid}/checkout")]
        //[Auth(Policy = "access_cart_api")]
        public async Task<IActionResult> CheckoutCart(Guid cartId)
        {
            return Ok(await _serviceClient.CheckoutAsync(new CheckoutRequest
            {
                CartId = cartId.ToString(),
            }));
        }

        [HttpDelete]
        [Route("{cartId:guid}/items/{productId:guid}")]
        //[Auth(Policy = "access_cart_api")]
        public async Task<IActionResult> RemoveItemInCart(Guid cartId, Guid productId)
        {
            return Ok(await _serviceClient.DeleteItemAsync(new DeleteItemRequest
            {
                CartId = cartId.ToString(),
                ProductId = productId.ToString()
            }));
        }
    }
}
