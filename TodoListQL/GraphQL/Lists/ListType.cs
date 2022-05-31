using System.Linq;
using HotChocolate;
using HotChocolate.Types;
using TodoListQL.Data;
using TodoListQL.Models;

namespace TodoListQL.GraphQL.Lists
{
    public class ListType : ObjectType<ItemList>
    {
        protected override void Configure(IObjectTypeDescriptor<ItemList> descriptor)
        {
            descriptor.Description("This model is used as item for the to list");

            descriptor.Field(x => x.ItemDatas)
                        .ResolveWith<Resolvers>(x => x.GetItems(default!, default!))
                        .UseDbContext<ApiDbContext>()
                        .Description("This is the list that the item belongs to");
        }

        private class Resolvers
        {
            public IQueryable<ItemData> GetItems(ItemList list, [ScopedService] ApiDbContext context)
            {
                return context.Items.Where(x => x.ListId == list.Id);
            }
        }
    }
}