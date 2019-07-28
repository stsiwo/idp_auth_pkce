using FluentNHibernate.Mapping;
using OrderingApiUnitTest.NHibernate.CustomType;
using OrderingApiUnitTest.NHibernate.Entity.CartAgg;

namespace OrderingApiUnitTest.NHibernate.DataEntity.Mapper
{
    class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            //Id(p => p.Id.Id);
            Id(p => p.Id, "id")
                .CustomType<CartProductIdUserType>();
            Map(p => p.Name).Column("name");

            Component<Price>(p => p.Price, p =>
            {
                p.Map(pr => pr.Amount, "price_amount");
                p.Map(pr => pr.Currency, "price_currency");

            });

            HasManyToMany(p => p.Carts);


            Table("cart_product");


        }
    }
}
