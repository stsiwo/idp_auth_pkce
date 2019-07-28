using FluentNHibernate.Mapping;
using OrderingApiUnitTest.NHibernate.CustomType;
using OrderingApiUnitTest.NHibernate.Entity.OrderAgg;

namespace OrderingApiUnitTest.NHibernate.DataEntity.Mapper
{
    class OrderProductMap : ClassMap<Product>
    {
        public OrderProductMap()
        {
            //Id(p => p.Id.Id);
            Id(p => p.Id, "id")
                .CustomType<OrderProductIdUserType>();
            Map(p => p.Name);

            Component<Price>(p => p.Price, p =>
            {
                p.Map(pr => pr.Amount, "price_amount");
                p.Map(pr => pr.Currency, "price_currency");

            });

            Table("order_product");


        }
    }
}
