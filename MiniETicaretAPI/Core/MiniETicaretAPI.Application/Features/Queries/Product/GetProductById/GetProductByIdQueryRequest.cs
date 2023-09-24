﻿using MediatR;

namespace MiniETicaretAPI.Application.Features.Queries.Product.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<GetProductByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
