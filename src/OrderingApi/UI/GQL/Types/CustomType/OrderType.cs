using GraphQL.DataLoader;
using GraphQL.Types;
using OrderingApi.Domain.OrderAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderingApi.UI.GQL.Types.CustomType
{
    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType(IDataLoaderContextAccessor dataLoaderContextAccessor /*,IPostRepository postRepository*/)
        {
            Field<StringGraphType, Guid>().Name("id");
//            Field(x => x.Name).Description("name of person");
//            Field(x => x.Gender, type: typeof(GenderEnumType)).Description("gender of person");
//            Field<ListGraphType<PostType>, IEnumerable<Post>>()
//           .Name("posts")
//           .ResolveAsync(context =>
//           {
//               // below code causes N+1 query problems
//               //return await postRepository.GetPostsByOrderIdAsync(context.Source.Id);
//
//               // avoid N+1 query problems
//
//               // Get or add a batch loader with the key "GetPostsById"
//               // The loader will call GetPostsByIdAsync for each batch of keys
//               var loader = dataLoaderContextAccessor.Context.GetOrAddCollectionBatchLoader<string, Post>("GetPostsById", postRepository.GetPostsByOrderIdsAsync);
//
//               // Add this PostId to the pending keys to fetch
//               // The task will complete once the GetPostsByIdAsync() returns with the batched results
//               return loader.LoadAsync(context.Source.Id);
//           });
            //Field<ListGraphType<PostType>>("posts");
        }
    }
}
